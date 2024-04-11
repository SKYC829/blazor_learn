using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public partial class L13_Blazor_SessionStorage : ComponentBase
    {
        private L13_CacheItemForm? _form;

        [Inject]
        public SessionStorageAccessor? SessionStorageAccessor { get; set; }

        private void UpdateState(string state)
        {
            if(_form is not null )
            {
                _form.UpdateOperationState(state);
            }
        }

        private async Task OnSubmitAsync(CacheItemModel cacheItemModel)
        {
            await SessionStorageAccessor!.SetAsync(cacheItemModel.Key!,cacheItemModel.Value);
            UpdateState($"设置会话存储成功！");
        }

        private async Task OnGetAsync(CacheItemModel cacheItemModel)
        {
            object? value = SessionStorageAccessor!.GetAsync(cacheItemModel.Key!);
            if ( value is not null )
            {
                if(value is Task )
                {
                    value = await ( value as Task<object?> );
                }
                UpdateState($"从会话存储中获取到 key {cacheItemModel.Key} 的值为 {value}");
            }
            else
            {
                UpdateState($"会话存储中没有 key {cacheItemModel.Key} 的数据");
            }
        }

        private async Task OnDeleteAsync(CacheItemModel cacheItemModel)
        {
            await SessionStorageAccessor!.DeleteAsync(cacheItemModel.Key!);
            UpdateState($"从会话存储中删除 key {cacheItemModel.Key} 的数据成功");
        }

        private async Task OnClearAsync()
        {
            await SessionStorageAccessor!.ClearAsync();
            UpdateState($"会话存储数据清空成功");
        }
    }
}
