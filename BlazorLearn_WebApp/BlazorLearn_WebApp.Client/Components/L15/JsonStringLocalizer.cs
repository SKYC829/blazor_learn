
using Microsoft.Extensions.Localization;

using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace BlazorLearn_WebApp.Client.Components.L15
{
    internal class JsonStringLocalizer : IStringLocalizer
    {
        private Type? _resourceSource;
        private HttpClient _httpClient;
        private string? _resourcesRelativePath;
        private ILogger<JsonStringLocalizer> _logger;
        private string? _baseName;
        private string? _location;
        private GlobalizationCultureProvider _globalizationCultureProvider;
        private JsonDocument? _document;
        private readonly ConcurrentDictionary<string,string?> _localizedStringCache = new ConcurrentDictionary<string, string?>();

        public JsonStringLocalizer(Type resourceSource, HttpClient httpClient, GlobalizationCultureProvider globalizationCultureProvider, string? resourcesRelativePath, ILogger<JsonStringLocalizer> logger)
        {
            _resourceSource = resourceSource;
            _httpClient = httpClient;
            _resourcesRelativePath = resourcesRelativePath;
            _logger = logger;
            _globalizationCultureProvider = globalizationCultureProvider;
        }

        public JsonStringLocalizer(string baseName, string location, HttpClient httpClient, GlobalizationCultureProvider globalizationCultureProvider, string? resourcesRelativePath, ILogger<JsonStringLocalizer> logger)
        {
            _baseName = baseName;
            _location = location;
            _httpClient = httpClient;
            _resourcesRelativePath = resourcesRelativePath;
            _logger = logger;
            _globalizationCultureProvider = globalizationCultureProvider;
        }

        public LocalizedString this[string name]
        {
            get
            {
                string? value = GetLocalizedString(name);
                return new LocalizedString(name, string.IsNullOrWhiteSpace(value) ? name : value, string.IsNullOrWhiteSpace(value), _resourcesRelativePath);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                string? formatter = GetLocalizedString(name);
                string? value = formatter;
                if ( !string.IsNullOrWhiteSpace(formatter) )
                {
                    value = string.Format(formatter, arguments);
                }
                return new LocalizedString(name, string.IsNullOrWhiteSpace(value) ? name : value, string.IsNullOrWhiteSpace(formatter), _resourcesRelativePath);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        private string GetResourcesLocationPath()
        {
            string extensions = $".{_globalizationCultureProvider.CurrentCulture.ThreeLetterWindowsLanguageName.ToLower()}.json";
            if(!string.IsNullOrWhiteSpace(_location) && !string.IsNullOrWhiteSpace(_baseName) )
            {
                return $"{Path.Combine(_resourcesRelativePath!, _location!, $"{_baseName}{extensions}")}";
            }
            else if(_resourceSource is not null )
            {
                return $"{Path.Combine(_resourcesRelativePath!, $"{_resourceSource!.Namespace?.Replace(_resourceSource!.Assembly.GetName()?.Name ?? "", "")}.{_resourceSource!.Name}".Replace('.', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar))}{extensions}";
            }
            return "";
        }

        private string? GetLocalizedString(string name)
        {
            string resourceLocation = GetResourcesLocationPath();
            if ( string.IsNullOrWhiteSpace(resourceLocation) )
            {
                return null;
            }
            string cacheKey = $"{resourceLocation}:{name}";
            if(!_localizedStringCache.TryGetValue(cacheKey,out string? value) || string.IsNullOrWhiteSpace(value))
            {
                _httpClient.GetStringAsync(resourceLocation)
                    .ContinueWith(t =>
                    {
                        if ( t.IsCompleted )
                        {
                            string json = t.Result;
                            if ( !string.IsNullOrWhiteSpace(json) )
                            {
                                Utf8JsonReader reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
                                if(JsonDocument.TryParseValue(ref reader,out _document) && _document is not null)
                                {
                                    if(_document.RootElement.TryGetProperty(name,out JsonElement element) )
                                    {
                                        value = element.GetString();
                                    }
                                }
                            }
                            _localizedStringCache.AddOrUpdate(cacheKey, value, (_,_)=> value);
                            _globalizationCultureProvider.NotifyCultureChanged();
                        }
                    });
            }
            return value;
        }
    }
}
