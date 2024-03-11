using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    public partial class L02_Observable_CascadingValue : ComponentBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        internal UserInfo UserInfo { get; set; } = new UserInfo ();
    }
}
