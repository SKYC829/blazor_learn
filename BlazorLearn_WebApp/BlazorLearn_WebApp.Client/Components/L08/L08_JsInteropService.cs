using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace BlazorLearn_WebApp.Client.Components.L08
{
    public partial class L08_JsInteropService
    {
        [SupportedOSPlatform("BROWSER")]
        public static async Task ImportL08_JsInteropService_Js()
        {
            await JSHost.ImportAsync("L08", "../Components/L08/JSInterop/L08_JsInterop.razor.js");
        }

        [JSImport("globalThis.promptHello")]
        public static partial Task PromptHelloInGlobal();

        [JSImport("promptHello","L08")]
        public static partial Task PromptHelloCollocated();

        [JSExport]
        public static void SayHelloInConsole()
        {
            Console.WriteLine("Hello! Blazor! CSharp!");
        }
    }
}
