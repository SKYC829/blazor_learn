using System.Collections.ObjectModel;

using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Components.Pages.L01
{
    public partial class L01_Component_Lifecycle_On_Server_1 : ComponentBase, IAsyncDisposable
    {
        private ObservableCollection<string> _stages;

        public L01_Component_Lifecycle_On_Server_1 ()
        {
            _stages = new ObservableCollection<string> ();
        }

        public override Task SetParametersAsync ( ParameterView parameters )
        {
            Task.Delay ( 3000 ).Wait ();
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> SetParametersAsync" );
            base.SetParametersAsync ( parameters );
            Task.Delay ( 1000 ).Wait ();
            return Task.CompletedTask;
        }

        protected override void OnInitialized ()
        {
            Thread.Sleep ( 3000 );
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnInitialized" );
            Thread.Sleep ( 1000 );
        }

        protected override async Task OnInitializedAsync ()
        {
            await Task.Delay ( 3000 );
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnInitializedAsync" );
            await Task.Delay ( 1000 );
        }

        protected override void OnParametersSet ()
        {
            Thread.Sleep ( 3000 );
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnParametersSet" );
            Thread.Sleep ( 1000 );
        }

        protected override async Task OnParametersSetAsync ()
        {
            await Task.Delay ( 3000 );
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnParametersSetAsync" );
            await Task.Delay ( 1000 );
        }

        protected override void OnAfterRender ( bool firstRender )
        {
            Thread.Sleep ( 3000 );
            _stages.Add ( $"{DateTime.Now:HH:mm:ss} -> OnAfterRender -> firstRender: {firstRender}" );
            Thread.Sleep ( 1000 );
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
