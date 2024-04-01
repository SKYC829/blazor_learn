using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L09
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public SingletonService? SingletonService { get; set; }
        [Inject]
        public ScopedService? ScopedService { get; set; }
        [Inject]
        public TransientService? TransientService { get; set; }
        [Inject]
        public ILogger<Index>? Logger { get; set; }
        public Task RaiseService()
        {
            return Task.Run(() =>
            {
                SingletonService?.SaySomething();
                ScopedService?.SaySomething();
                TransientService?.SaySomething();
                Logger?.LogWarning($"--------{DateTime.Now}--------{"分割线"}--------");
            });
        }
    }
}
