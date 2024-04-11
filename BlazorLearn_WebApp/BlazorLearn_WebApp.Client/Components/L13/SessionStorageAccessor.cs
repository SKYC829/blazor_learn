using Microsoft.JSInterop;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public class SessionStorageAccessor(IJSRuntime jsRuntime)
    {
        private Lazy<ValueTask<IJSObjectReference>>? _moduleTask;

        private void EnsureModuleLoaded()
        {
            if(_moduleTask is null || !_moduleTask.IsValueCreated)
            {
                _moduleTask = new Lazy<ValueTask<IJSObjectReference>>(jsRuntime.InvokeAsync<IJSObjectReference>("import","./js/l13/sessionstorageaccessor.js"));
            }
        }

        public async Task SetAsync(string key,object? value)
        {
            EnsureModuleLoaded();
            await ( await _moduleTask!.Value ).InvokeVoidAsync("setAsync", key, value);
        }

        public async Task<object?> GetAsync(string key)
        {
            EnsureModuleLoaded();
            return await ( await _moduleTask!.Value ).InvokeAsync<object?>("getAsync", key);
        }

        public async Task DeleteAsync(string key)
        {
            EnsureModuleLoaded();
            await ( await _moduleTask!.Value ).InvokeVoidAsync("deleteAsync", key);
        }

        public async Task ClearAsync()
        {
            EnsureModuleLoaded();
            await ( await _moduleTask!.Value ).InvokeVoidAsync("clearAsync");
        }
    }
}
