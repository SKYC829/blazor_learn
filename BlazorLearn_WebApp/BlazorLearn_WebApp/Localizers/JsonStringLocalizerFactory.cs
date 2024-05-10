using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

using System.Collections.Concurrent;

namespace BlazorLearn_WebApp.Localizers
{
    internal class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly string? _resourcesRelativePath;
        private readonly string? _applicationBasePath;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<JsonStringLocalizerFactory> _logger;
        private readonly ConcurrentDictionary<string, JsonStringLocalizer> _localizerCache;

        public JsonStringLocalizerFactory(IOptions<LocalizationOptions> options,ILoggerFactory loggerFactory,ILogger<JsonStringLocalizerFactory> logger)
        {
            _resourcesRelativePath = options.Value.ResourcesPath;
            _applicationBasePath = Environment.CurrentDirectory;
            _loggerFactory = loggerFactory;
            _localizerCache = new ConcurrentDictionary<string, JsonStringLocalizer>();
            _logger = logger;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            string? location = resourceSource.Namespace?.Replace(resourceSource.Assembly.GetName()?.Name??"","").Replace('.',Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar);
            string baseName = resourceSource.Name;
            string resourceBasePath = Path.Combine(_applicationBasePath!,_resourcesRelativePath!);
            string resourceLocationPath = Path.Combine(resourceBasePath,location??"");
            if(!_localizerCache.TryGetValue(resourceLocationPath,out var stringLocalizer) )
            {
                PhysicalFileProvider fileProvider = new PhysicalFileProvider(resourceLocationPath);
                IEnumerable<IFileInfo> resourceFiles = fileProvider.GetDirectoryContents("").Where(p=>!p.IsDirectory && p.Exists && p.Name.StartsWith(baseName) && Path.GetExtension(p.Name) == ".json");
                stringLocalizer = new JsonStringLocalizer(resourceLocationPath,resourceFiles,_loggerFactory.CreateLogger<JsonStringLocalizer>());
                _localizerCache[resourceLocationPath] = stringLocalizer;
            }
            return stringLocalizer;
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(baseName, nameof(baseName));
            ArgumentException.ThrowIfNullOrWhiteSpace(location, nameof(location));
            string resourceBasePath = Path.Combine(_applicationBasePath!,_resourcesRelativePath!);
            string resourceLocationPath = Path.Combine(resourceBasePath,location);
            return _localizerCache.GetOrAdd(resourceLocationPath, _ =>
            {
                PhysicalFileProvider fileProvider = new PhysicalFileProvider(resourceLocationPath);
                IEnumerable<IFileInfo> resourceFiles = fileProvider.GetDirectoryContents("").Where(p=>!p.IsDirectory && p.Exists && p.Name.StartsWith(baseName) && Path.GetExtension(p.Name) == ".json");
                return new JsonStringLocalizer(resourceLocationPath, resourceFiles, _loggerFactory.CreateLogger<JsonStringLocalizer>());
            });
        }
    }
}
