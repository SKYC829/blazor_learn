using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    public partial class L02_ChildNotifyParent : ComponentBase
    {
        private UserInfo _userInfo;

        public bool CanSubmit => _userInfo == null || string.IsNullOrWhiteSpace ( _userInfo.Name ) || string.IsNullOrWhiteSpace ( _userInfo.Email );

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override void OnInitialized ()
        {
            _userInfo = new ();
        }

        internal async Task OnPropertyChanged ( (string, string) arg )
        {
            SetValue ( arg.Item1 , arg.Item2 );
            StateHasChanged ();
        }

        private void SetValue ( string name , string value )
        {
            typeof ( UserInfo ).GetProperty ( name ).SetValue ( _userInfo , value );
        }

        private async Task HandleClickAsync ()
        {
            await JSRuntime.InvokeVoidAsync ( "alert" , $"名称:{_userInfo.Name}，邮箱:{_userInfo.Email}" );
            _userInfo = new ();
        }
    }
}
