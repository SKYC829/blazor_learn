using Microsoft.JSInterop;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public class CookieStorageAccessor(IJSRuntime jsRuntime)
    {
        private Lazy<ValueTask<IJSObjectReference>>? _jsModuleTask;

        private void EnsureModuleLoaded()
        {
            _jsModuleTask = new Lazy<ValueTask<IJSObjectReference>>(jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/l13/cookiestorageaccessor.js"));
        }

        public async Task SetCookieAsync(string name, object? value = null, DateTime? expires = null, string? path = null)
        {
            EnsureModuleLoaded();
            await (await _jsModuleTask!.Value).InvokeVoidAsync("set", name, value, (object?)expires??(object?)"never", path);
        }

        public async Task<string?> GetCookieAsync(string name)
        {
            EnsureModuleLoaded();
            return await (await _jsModuleTask!.Value).InvokeAsync<string?>("get", name);
        }

        public async Task RemoveCookieAsync(string name,string? path = null)
        {
            EnsureModuleLoaded();
            await (await _jsModuleTask!.Value).InvokeVoidAsync("remove", name,path);
        }

        public async Task ClearAsync()
        {
            EnsureModuleLoaded();
            await (await _jsModuleTask!.Value).InvokeVoidAsync("clear");
        }
    }
}
