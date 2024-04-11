using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public partial class L13_Blazor_LocalStorage : ComponentBase
    {
        private L13_CacheItemForm? _form;
        [Inject]
        public LocalStorageAccessor? LocalStorageAccessor { get; set; }

        private void UpdateState(string state)
        {
            if(_form is not null )
            {
                _form.UpdateOperationState(state);
            }
        }

        private async Task OnSubmitAsync(CacheItemModel cacheItemModel)
        {
            await LocalStorageAccessor!.SetAsync(cacheItemModel.Key!,cacheItemModel.Value);
            UpdateState($"设置本地存储成功！");
        }

        private async Task OnGetAsync(CacheItemModel cacheItemModel)
        {
            object? value = await LocalStorageAccessor!.GetAsync(cacheItemModel.Key!);
            if(value is not null )
            {
                UpdateState($"从本地存储中获取到 key {cacheItemModel.Key} 的值为 {value}");
            }
            else
            {
                UpdateState($"本地存储中没有 key {cacheItemModel.Key} 的数据");
            }
        }

        private async Task OnDeleteAsync(CacheItemModel cacheItemModel)
        {
            await LocalStorageAccessor!.DeleteAsync(cacheItemModel.Key!);
            UpdateState($"从本地存储中删除 key {cacheItemModel.Key} 的数据成功");
        }

        private async Task OnClearAsync()
        {
            await LocalStorageAccessor!.ClearAsync();
            UpdateState($"本地存储数据清空成功");
        }
    }
}
