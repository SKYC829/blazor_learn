namespace BlazorLearn_WebApp.Client.Components.L09
{
    public interface ITestService:IDisposable
    {
        Guid ServiceId { get; }
        void SaySomething();
    }
}
