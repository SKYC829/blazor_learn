using Microsoft.JSInterop;

using System;
using System.Text.Json;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public sealed class CacheStorageAccessor(IJSRuntime jsRuntime)
    {
        private Lazy<ValueTask<IJSObjectReference>>? _jsModuleTask;

        private async Task EnsureModuleLoadedAsync()
        {
            if(_jsModuleTask is null || !_jsModuleTask.IsValueCreated )
            {
                _jsModuleTask = new Lazy<ValueTask<IJSObjectReference>>(jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/L13/CacheStorageAccessor.js"));
            }
        }

        public async Task StoreAsync(string url,string method,string? body = "",object? response = null)
        {
            await EnsureModuleLoadedAsync();
            await (await _jsModuleTask!.Value).InvokeVoidAsync("storeAsync",url,HttpMethod.Parse(method).Method,body,JsonSerializer.Serialize(response));
        }

        public async Task<string> GetAsync(string url, string method, string? body = "")
        {
            await EnsureModuleLoadedAsync();
            return await ( await _jsModuleTask!.Value ).InvokeAsync<string>("getAsync", url, HttpMethod.Parse(method).Method, body);
        }

        public async Task DeleteAsync(string url, string method, string? body = "")
        {
            await EnsureModuleLoadedAsync();
            await ( await _jsModuleTask!.Value ).InvokeVoidAsync("deleteAsync", url, HttpMethod.Parse(method).Method, body);
        }

        public async Task ClearAsync()
        {
            await EnsureModuleLoadedAsync();
            await ( await _jsModuleTask!.Value ).InvokeVoidAsync("clearAsync");
        }
    }
}
