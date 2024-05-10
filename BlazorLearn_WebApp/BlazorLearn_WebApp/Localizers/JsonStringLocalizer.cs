using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;

using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;

namespace BlazorLearn_WebApp.Localizers
{
    internal class JsonStringLocalizer : IStringLocalizer
    {
        private readonly string _resourceLocationPath;
        private readonly IEnumerable<IFileInfo> _resourceFiles;
        private readonly ILogger<JsonStringLocalizer> _logger;
        private readonly IFileInfo? _defaultResourceFile;
        private readonly ConcurrentDictionary<string,string> _resourceFileCache;

        public LocalizedString this[string name]
        {
            get
            {
                string? value = GetLocalizedString(name);
                return new LocalizedString(name, value ?? name, value == null, _resourceLocationPath);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                string? formatter = GetLocalizedString(name);
                string? value = string.Format(CultureInfo.CurrentUICulture,formatter??name,arguments);
                return new LocalizedString(name, value ?? name, formatter == null, _resourceLocationPath);
            }
        }

        public JsonStringLocalizer(string resourceLocationPath, IEnumerable<IFileInfo> resourceFiles, ILogger<JsonStringLocalizer> logger)
        {
            _resourceLocationPath = resourceLocationPath;
            _resourceFiles = resourceFiles;
            _logger = logger;
            _defaultResourceFile = _resourceFiles.FirstOrDefault();
            _resourceFileCache = new ConcurrentDictionary<string, string>();
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        private string? GetLocalizedString(string name,CultureInfo? culture = null)
        {
            try
            {
                _logger.LogInformation($"获取本地化字符串:{name} 入参区域:{culture?.Name??"N/A"} 环境区域:{CultureInfo.CurrentUICulture?.Name??"N/A"} 默认线程环境区域: {CultureInfo.DefaultThreadCurrentUICulture?.Name??"N/A"}");
                if ( string.IsNullOrWhiteSpace(name) || !_resourceFiles.Any() )
                {
                    return null;
                }
                culture ??= ( CultureInfo.CurrentUICulture ?? CultureInfo.DefaultThreadCurrentUICulture ) ?? CultureInfo.GetCultureInfo("zh-CN");
                string cultureName = culture.ThreeLetterWindowsLanguageName.ToLower();
                IFileInfo? resourcesFile = _resourceFiles.FirstOrDefault(p=>p.Name.EndsWith($"{cultureName}.json"))??_defaultResourceFile;
                if ( resourcesFile is null )
                {
                    return null;
                }
                if ( !_resourceFileCache.TryGetValue(resourcesFile.PhysicalPath!, out string? jsonValue) )
                {
                    using ( StreamReader reader = new StreamReader(resourcesFile.CreateReadStream()) )
                    {
                        jsonValue = reader.ReadToEnd();
                        _resourceFileCache[resourcesFile.PhysicalPath!] = jsonValue;
                    }
                }
                if ( string.IsNullOrWhiteSpace(jsonValue) )
                {
                    return null;
                }
                JsonDocument document = JsonDocument.Parse(jsonValue,new JsonDocumentOptions()
                {
                    AllowTrailingCommas = true,
                    CommentHandling = JsonCommentHandling.Skip
                });
                string? value = document.RootElement.GetProperty(name).GetString();
                return value;
            }
            catch ( Exception ex)
            {
                return null;
            }
        }
    }
}
