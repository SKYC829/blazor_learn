
namespace BlazorLearn_WebApp.Client.Components.L09
{
    public class ScopedService : ITestService
    {
        private readonly ILogger<ScopedService> _logger;
        public Guid ServiceId => Guid.NewGuid();

        public ScopedService(ILogger<ScopedService> logger)
        {
            _logger = logger;
            _logger.LogInformation($"{nameof(ScopedService)} 服务 初始化");
        }


        public void Dispose()
        {
            _logger.LogInformation($"{nameof(ScopedService)} 服务 被释放");
        }

        public void SaySomething()
        {
            _logger.LogInformation($"当前 {nameof(ScopedService)} 的 {nameof(ServiceId)} 是 {ServiceId:N}");
        }
    }
}
