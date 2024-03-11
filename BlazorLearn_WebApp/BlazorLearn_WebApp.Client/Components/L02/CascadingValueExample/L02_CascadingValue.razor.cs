using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    public partial class L02_CascadingValue : ComponentBase, IAsyncDisposable
    {
        public string? Message { get; private set; } = "来自父组件的消息数据";

        public async ValueTask DisposeAsync ()
        {
            Message = null;
        }
    }
}
