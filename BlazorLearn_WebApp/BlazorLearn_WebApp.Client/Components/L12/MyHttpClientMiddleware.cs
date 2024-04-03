
namespace BlazorLearn_WebApp.Client.Components.L12
{
    public sealed class MyHttpClientMiddleware : DelegatingHandler
    {
        private readonly ILogger<MyHttpClientMiddleware> _logger;

        public MyHttpClientMiddleware(ILogger<MyHttpClientMiddleware> logger)
        {
            _logger = logger;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"中间件{nameof(MyHttpClientMiddleware)}被执行了");
            return base.SendAsync(request, cancellationToken);
        }
    }

    public sealed class YourHttpClientMiddleware : DelegatingHandler
    {
        private readonly ILogger<YourHttpClientMiddleware> _logger;

        public YourHttpClientMiddleware(ILogger<YourHttpClientMiddleware> logger)
        {
            _logger = logger;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"中间件{nameof(YourHttpClientMiddleware)}被执行了");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
