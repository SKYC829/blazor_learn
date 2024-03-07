using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    public partial class L02_Observable_CascadingValue : ComponentBase
    {
        public string Name { get; set; } = "989";

        public string Email { get; set; }

        protected override void OnParametersSet ()
        {
            base.OnParametersSet ();
        }
    }
}
