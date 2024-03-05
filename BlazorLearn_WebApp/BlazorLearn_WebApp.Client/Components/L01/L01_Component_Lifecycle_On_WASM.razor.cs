using System.Collections.ObjectModel;

using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L01
{
    public partial class L01_Component_Lifecycle_On_WASM : ComponentBase, IAsyncDisposable
    {
        private ObservableCollection<string> _stages;

        public L01_Component_Lifecycle_On_WASM ()
        {
            _stages = new ObservableCollection<string> ();
        }

        public override Task SetParametersAsync ( ParameterView parameters )
        {
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> SetParametersAsync" );
            base.SetParametersAsync ( parameters );
            return Task.CompletedTask;
        }

        protected override void OnInitialized ()
        {
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnInitialized" );
        }

        protected override async Task OnInitializedAsync ()
        {
            await Task.Delay ( 3000 );
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnInitializedAsync" );
            await Task.Delay ( 1000 );
        }

        protected override void OnParametersSet ()
        {
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnParametersSet" );
        }

        protected override async Task OnParametersSetAsync ()
        {
            await Task.Delay ( 3000 );
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnParametersSetAsync" );
            await Task.Delay ( 1000 );
        }

        protected override void OnAfterRender ( bool firstRender )
        {
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnAfterRender -> firstRender: {firstRender}" );
        }

        protected override async Task OnAfterRenderAsync ( bool firstRender )
        {
            await Task.Delay ( 3000 );
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnAfterRenderAsync -> firstRender: {firstRender}" );
            await Task.Delay ( 1000 );
        }

        public async ValueTask DisposeAsync ()
        {
            await Task.Delay ( 2000 );
            await Console.Out.WriteLineAsync ( $"{DateTime.Now:HH:mm:ss} -> DisposeAsync" );
            _stages.Clear ();
        }
    }
}
