using Microsoft.JSInterop;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public class LocalStorageAccessor(IJSRuntime jsRuntime)
    {
        private Lazy<ValueTask<IJSObjectReference>>? _moduleTask;

        private Task EnsureModuleLoadedAsync()
        {
            if(_moduleTask is null || !_moduleTask.IsValueCreated )
            {
                _moduleTask = new Lazy<ValueTask<IJSObjectReference>>(jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/l13/localstorageaccessor.js"));
            }
            return Task.CompletedTask;
        }

        public async Task SetAsync(string key,object? value)
        {
            await EnsureModuleLoadedAsync();
            await ( await _moduleTask!.Value ).InvokeVoidAsync("setAsync", key, value);
        }

        public async Task<object> GetAsync(string key)
        {
            await EnsureModuleLoadedAsync();
            return await ( await _moduleTask!.Value ).InvokeAsync<object>("getAsync", key);
        }

        public async Task DeleteAsync(string key)
        {
            await EnsureModuleLoadedAsync();
            await ( await _moduleTask!.Value ).InvokeVoidAsync("deleteAsync", key);
        }

        public async Task ClearAsync()
        {
            await EnsureModuleLoadedAsync();
            await ( await _moduleTask!.Value ).InvokeVoidAsync("clearAsync");
        }
    }
}
