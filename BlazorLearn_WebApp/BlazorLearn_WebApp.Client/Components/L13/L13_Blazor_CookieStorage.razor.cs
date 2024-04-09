using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public partial class L13_Blazor_CookieStorage : ComponentBase
    {
        private L13_CacheItemForm? _form;

        [Inject]
        public CookieStorageAccessor CookieStorageAccessor { get; set; }

        private async Task GetCookieAsync(CacheItemModel cacheItemModel)
        {
            string? result = await CookieStorageAccessor!.GetCookieAsync(cacheItemModel.Key!);
            if(string.IsNullOrWhiteSpace(result))
            {
                UpdateState($"Cookie缓存中没有key为{cacheItemModel.Key}的缓存！");
            }
            else
            {
                UpdateState($"获取缓存成功，Cookie缓存中key{cacheItemModel.Key}的值为{result}");
            }
        }

        private async Task SetCookieAsync(CacheItemModel cacheItemModel)
        {
            await CookieStorageAccessor!.SetCookieAsync(cacheItemModel.Key!, cacheItemModel.Value, cacheItemModel.TimeoutAt,cacheItemModel.Url);
            UpdateState($"设置Cookie缓存成功！");
        }

        private async Task RemoveAsync(CacheItemModel cacheItemModel)
        {
            await CookieStorageAccessor!.RemoveCookieAsync(cacheItemModel.Key!,cacheItemModel.Url);
            UpdateState($"删除Cookie缓存成功！");
        }

        private async Task ClearAsync()
        {
            await CookieStorageAccessor!.ClearAsync();
            UpdateState($"清空Cookie缓存成功！");
        }

        private void UpdateState(string state)
        {
            if (_form is not null)
            {
                _form.UpdateOperationState(state);
            }
        }
    }
}
