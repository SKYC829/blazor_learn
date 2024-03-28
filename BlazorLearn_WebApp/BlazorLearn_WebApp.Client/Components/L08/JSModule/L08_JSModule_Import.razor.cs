using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorLearn_WebApp.Client.Components.L08.JSModule
{
    public partial class L08_JSModule_Import:ComponentBase
    {
        private Lazy<ValueTask<IJSObjectReference>>? _jsModule;
        private Lazy<ValueTask<IJSObjectReference>>? _colocatedJsModule;

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _jsModule = new Lazy<ValueTask<IJSObjectReference>>(JsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/example.js"));
            _colocatedJsModule = new Lazy<ValueTask<IJSObjectReference>>(JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/L08/JSModule/L08_JSModule_Import.razor.js"));
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if ( _jsModule is not null && _jsModule.IsValueCreated )
            {
                await ( await _jsModule.Value ).InvokeVoidAsync("sayHello", firstRender);
            }
            if ( _colocatedJsModule is not null && _colocatedJsModule.IsValueCreated )
            {
                await ( await _colocatedJsModule.Value ).InvokeVoidAsync("sayHello1", firstRender);
                SampleModel obj = new() { Name = "abc", Age = 18 };
                await ( await _colocatedJsModule.Value ).InvokeVoidAsync("passingCSharpObj", obj);
                Console.WriteLine(JsonSerializer.Serialize<object>(obj));
                var wrapper = DotNetObjectReference.Create<SampleModel>(obj);
                await ( await _colocatedJsModule.Value ).InvokeVoidAsync("passingCSharpObjAndSayHi", wrapper);
                Console.WriteLine(JsonSerializer.Serialize<object>(wrapper.Value));
            }
        }

    }
}
