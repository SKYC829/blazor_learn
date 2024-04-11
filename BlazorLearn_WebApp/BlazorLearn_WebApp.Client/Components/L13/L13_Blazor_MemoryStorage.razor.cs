using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public partial class L13_Blazor_MemoryStorage : ComponentBase
    {
        private L13_CacheItemForm? _form;

        [Inject]
        public MemoryStorageAccessor? MemoryStorageAccessor { get; set; }

        private void UpdateState(string state)
        {
            if(_form is not null )
            {
                _form.UpdateOperationState(state);
            }
        }

        private void OnSubmit(CacheItemModel cacheItemModel)
        {
            MemoryStorageAccessor!.Set(cacheItemModel.Key!, cacheItemModel.Value);
            UpdateState($"设置内存存储成功！");
        }

        private void OnGet(CacheItemModel cacheItemModel)
        {
            object? value = MemoryStorageAccessor!.Get(cacheItemModel.Key!);
            if(value is not null )
            {
                UpdateState($"从内存存储中获取到 key {cacheItemModel.Key} 的值为 {value}");
            }
            else
            {
                UpdateState($"内存存储中没有 key {cacheItemModel.Key} 的数据");
            }
        }

        private void OnDelete(CacheItemModel cacheItemModel)
        {
            MemoryStorageAccessor!.Delete(cacheItemModel.Key!);
            UpdateState($"从内存存储中删除 key {cacheItemModel.Key} 的数据成功");
        }

        private void OnClear()
        {
            MemoryStorageAccessor?.Clear();
            UpdateState($"内存存储数据清空成功");
        }
    }
}
