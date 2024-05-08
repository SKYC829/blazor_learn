
using BlazorLearn_WebApp.Client.Components.L13;

using Microsoft.JSInterop;

using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace BlazorLearn_WebApp.Client.Components.L14.GoogleLogin
{
    public partial class GoogleLoginApi : IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _jsModule;
        private readonly string _googleClientId;

        public GoogleLoginApi(IJSRuntime jsRuntime,IConfiguration configuration)
        {
            _jsRuntime = jsRuntime;
            _googleClientId = configuration.GetSection("GoogleClient").GetValue<string>("clientId") ?? "";
        }

        private async Task EnsureModuleLoaded()
        {
            if(_jsModule is null )
            {
                _jsModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/GoogleClientLib.js");
            }
        }

        public async Task InitializeAsync(string? divName=null)
        {
            await EnsureModuleLoaded();
            if(string.IsNullOrWhiteSpace(divName))
            {
                divName = "googleDiv";
            }
            await _jsModule!.InvokeVoidAsync("GoogleLoginInit",_googleClientId,divName);
        }

        public async ValueTask DisposeAsync()
        {
            if(_jsModule is not null )
            {
                await _jsModule.DisposeAsync();
            }
        }
    }
}
