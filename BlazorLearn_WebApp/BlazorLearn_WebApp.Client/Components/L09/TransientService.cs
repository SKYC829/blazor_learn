namespace BlazorLearn_WebApp.Client.Components.L09
{
    public class TransientService : ITestService
    {

        private readonly ILogger<TransientService> _logger;
        public Guid ServiceId => Guid.NewGuid();
        public TransientService(ILogger<TransientService> logger)
        {
            _logger = logger;
            _logger.LogInformation($"{nameof(TransientService)} 服务 初始化");
        }

        public void Dispose()
        {
            _logger.LogInformation($"{nameof(TransientService)} 服务 被释放");
        }
        public void SaySomething()
        {
            _logger.LogInformation($"当前 {nameof(TransientService)} 的 {nameof(ServiceId)} 是 {ServiceId:N}");
        }
    }
}
