using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public partial class L13_Blazor_CacheStorage : ComponentBase
    {
        [Inject]
        public CacheStorageAccessor? CacheStorageAccessor { get; set; }

        private L13_CacheItemForm? _form;

        private async Task OnSubmitAsync(CacheItemModel cacheItemModel)
        {
            Task t = CacheStorageAccessor!.StoreAsync(cacheItemModel.Url!.TrimStart('/','\\'), cacheItemModel.Method!.ToUpper(), cacheItemModel.Body, cacheItemModel.Value);
            await t;
            if ( t.IsFaulted )
            {
                UpdateState($"保存缓存失败！{t.Exception.InnerException?.Message}");
            }
            else
            {
                UpdateState("保存缓存成功！");
            }
        }

        private async Task OnGetAsync(CacheItemModel cacheItemModel)
        {
            string value = await CacheStorageAccessor!.GetAsync(cacheItemModel.Url!.TrimStart('/','\\'),cacheItemModel.Method!.ToUpper(),cacheItemModel.Body);
            if ( !string.IsNullOrWhiteSpace(value) )
            {
                UpdateState($"成功从缓存中读取到值，请求: {cacheItemModel.Method} {cacheItemModel.Url} /b={cacheItemModel.Body} 缓存的结果为 {value}");
            }
            else
            {
                UpdateState($"缓存中没有请求为{cacheItemModel.Method} {cacheItemModel.Url} /b={cacheItemModel.Body}的缓存结果");
            }
        }

        private async Task OnDeleteAsync(CacheItemModel cacheItemModel)
        {
            await CacheStorageAccessor!.DeleteAsync(cacheItemModel.Url!.TrimStart('/', '\\'), cacheItemModel.Method!.ToUpper(), cacheItemModel.Body);
            UpdateState($"缓存删除成功");
        }

        private async Task OnClearAsync()
        {
            await CacheStorageAccessor!.ClearAsync();
            UpdateState($"已清空所有缓存");
        }

        private void UpdateState(string state)
        {
            if(_form is not null )
            {
                _form.UpdateOperationState(state);
            }
        }
    }
}
