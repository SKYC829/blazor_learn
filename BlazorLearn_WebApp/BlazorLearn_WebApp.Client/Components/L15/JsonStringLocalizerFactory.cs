using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

using System.Collections.Concurrent;

namespace BlazorLearn_WebApp.Client.Components.L15
{
    internal class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<JsonStringLocalizerFactory> _logger;
        private readonly string? _resourcesRelativePath;
        private readonly ConcurrentDictionary<string,IStringLocalizer> _localizerCache;
        private readonly GlobalizationCultureProvider _globalizationCultureProvider;

        public JsonStringLocalizerFactory(IOptions<LocalizationOptions> options,ILogger<JsonStringLocalizerFactory> logger,ILoggerFactory loggerFactory,IHttpClientFactory httpClientFactory,GlobalizationCultureProvider globalizationCultureProvider)
        {
            _resourcesRelativePath = options.Value.ResourcesPath;
            _loggerFactory = loggerFactory;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("self");
            _localizerCache = new ConcurrentDictionary<string, IStringLocalizer>();
            _globalizationCultureProvider = globalizationCultureProvider;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            string cacheKey = GetLocalizerCacheKey(resourceSource);
            if(!_localizerCache.TryGetValue(cacheKey,out var localizer) )
            {
                localizer = new JsonStringLocalizer(resourceSource, _httpClient, _globalizationCultureProvider, _resourcesRelativePath, _loggerFactory.CreateLogger<JsonStringLocalizer>());
                _localizerCache[cacheKey] = localizer;
            }
            return localizer;
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            string cacheKey = GetLocalizerCacheKey(baseName,location);
            return _localizerCache.GetOrAdd(cacheKey, _ =>
            {
                return new JsonStringLocalizer(baseName, location, _httpClient,_globalizationCultureProvider, _resourcesRelativePath, _loggerFactory.CreateLogger<JsonStringLocalizer>());
            });
        }

        private string GetLocalizerCacheKey(Type resourceSource)
        {
            return $"{resourceSource.Namespace?.Replace(resourceSource.Assembly.GetName()?.Name??"","")}.{resourceSource.Name}".Replace('.',Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar);
        }

        private string GetLocalizerCacheKey(string baseName, string location)
        {
            return Path.Combine(location,baseName);
        }
    }
}
