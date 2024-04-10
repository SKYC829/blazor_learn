using Microsoft.JSInterop;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public class IndexedDBAccessor(IJSRuntime jsRuntime)
    {
        private Lazy<ValueTask<IJSObjectReference>>? _moduleTask;

        private async Task EnsureModuleLoadedAsync()
        {
            if (_moduleTask is null || !_moduleTask.IsValueCreated)
            {
                _moduleTask = new Lazy<ValueTask<IJSObjectReference>>(jsRuntime.InvokeAsync<IJSObjectReference>("import","./js/l13/indexeddbaccessor.js"));
            }
        }

        public async Task InitializeAsync(string dbName,int version)
        {
            await EnsureModuleLoadedAsync();
            await ( await _moduleTask!.Value ).InvokeVoidAsync("initializeDb", dbName, version);
        }

        public async Task<object?> SetAsync(string dbName,int version,object? value)
        {
            await EnsureModuleLoadedAsync();
            return await ( await _moduleTask!.Value ).InvokeAsync<object>("setAsync",dbName,version,value);
        }

        public async Task<object?> GetAsync(string dbName,int version,string key)
        {
            await EnsureModuleLoadedAsync();
            return await ( await _moduleTask!.Value ).InvokeAsync<object>("getAsync", dbName, version, key);
        }

        public async Task DeleteAsync(string dbName,int version,string key)
        {

            await EnsureModuleLoadedAsync();
            await (await _moduleTask!.Value).InvokeVoidAsync("deleteAsync",dbName,version,key);
        }

        public async Task ClearAsync(string dbName,int version)
        {
            await EnsureModuleLoadedAsync();
            await (await _moduleTask.Value).InvokeVoidAsync("clearAsync",dbName,version);
        }
    }
}
