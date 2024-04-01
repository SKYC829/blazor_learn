namespace BlazorLearn_WebApp.Client.Components.L09
{
    public class SingletonService : ITestService
    {
        private readonly ILogger<SingletonService> _logger;
        public Guid ServiceId { get; init; } = Guid.NewGuid();
        public SingletonService(ILogger<SingletonService> logger)
        {
            _logger = logger;
            _logger.LogInformation($"{nameof(SingletonService)} 服务 初始化");
        }

        public void Dispose()
        {
            _logger.LogInformation($"{nameof(SingletonService)} 服务 被释放");
        }
        public void SaySomething()
        {
            _logger.LogInformation($"当前 {nameof(SingletonService)} 的 {nameof(ServiceId)} 是 {ServiceId:N}");
        }
    }
}
