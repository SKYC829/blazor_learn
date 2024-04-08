using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public partial class L13_CacheItemForm:ComponentBase
    {
        private CacheItemModel? _cacheItemModel;
        private string? _searchKey;
        private string? _operationState;

        [Parameter]
        public bool UseCacheStorage { get; set; }

        [Parameter]
        public EventCallback<CacheItemModel> OnSubmitEvent { get; set; }

        [Parameter]
        public EventCallback<CacheItemModel> OnGetEvent { get; set; }

        [Parameter]
        public EventCallback<CacheItemModel> OnDeleteEvent { get; set; }

        [Parameter]
        public EventCallback OnClearEvent { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _cacheItemModel = new CacheItemModel();
        }

        private async Task OnSubmitAsync()
        {
            if ( OnSubmitEvent.HasDelegate )
            {
                await OnSubmitEvent.InvokeAsync(_cacheItemModel);
            }
        }

        private async Task OnGetAsync()
        {
            _cacheItemModel!.Key = _searchKey;
            if(OnGetEvent.HasDelegate )
            {
                await OnGetEvent.InvokeAsync(_cacheItemModel);
            }
        }

        private async Task OnDeleteAsync()
        {
            _cacheItemModel!.Key = _searchKey;
            if (OnDeleteEvent.HasDelegate )
            {
                await OnDeleteEvent.InvokeAsync(_cacheItemModel);
            }
        }

        private async Task OnClearAsync()
        {
            if ( OnClearEvent.HasDelegate )
            {
                await OnClearEvent.InvokeAsync();
            }
        }

        public void UpdateOperationState(string state)
        {
            _operationState = state;
        }
    }
}
