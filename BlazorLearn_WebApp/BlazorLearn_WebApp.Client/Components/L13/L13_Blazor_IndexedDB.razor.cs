using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public partial class L13_Blazor_IndexedDB:ComponentBase
    {
        private L13_CacheItemForm? _form;

        [Inject]
        public IndexedDBAccessor? IndexedDBAccessor { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if ( firstRender )
            {
                await IndexedDBAccessor!.InitializeAsync("测试名称", 1);
                UpdateState($"Indexed DB 初始化成功");
            }
        }

        private async Task OnSubmitAsync(CacheItemModel cacheItemModel)
        {
            object? key = await IndexedDBAccessor!.SetAsync("测试名称", 1, cacheItemModel);
            UpdateState($"新增数据成功，标识:{key}");
        }

        private async Task OnGetAsync(CacheItemModel cacheItemModel)
        {
            object? result = await IndexedDBAccessor!.GetAsync("测试名称", 1, cacheItemModel.Key);
            UpdateState($"获取数据完成，key:{cacheItemModel.Key}存储的数据是:{result}");
        }

        private async Task OnDeleteAsync(CacheItemModel cacheItemModel)
        {
            await IndexedDBAccessor!.DeleteAsync("测试名称", 1, cacheItemModel.Key);
            UpdateState($"删除key是{cacheItemModel.Key}的存储数据");
        }

        private async Task OnClearAsync()
        {
            await IndexedDBAccessor!.ClearAsync("测试名称",1);
            UpdateState($"IndexedDB 存储数据清空成功");
        }

        private void UpdateState(string state)
        {
            if ( _form is not null )
            {
                _form.UpdateOperationState(state);
            }
        }
    }
}
