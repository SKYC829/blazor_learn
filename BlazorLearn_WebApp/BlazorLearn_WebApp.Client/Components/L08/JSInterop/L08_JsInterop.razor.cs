using Microsoft.AspNetCore.Components;

using System.Runtime.Versioning;

namespace BlazorLearn_WebApp.Client.Components.L08.JSInterop
{
    public partial class L08_JsInterop : ComponentBase
    {

        protected override void OnInitialized()
        {
            base.OnInitialized();
            L08_JsInteropService.PromptHelloInGlobal();
            L08_JsInteropService.ImportL08_JsInteropService_Js().ContinueWith(async c =>
            {
                await L08_JsInteropService.PromptHelloCollocated();
            });
        }
    }
}
