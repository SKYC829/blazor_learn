using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    public partial class L02_Multiple_CascadingValue_Parent : ComponentBase
    {
        [CascadingParameter ( Name = "Value1" )]
        public string Value1 { get; set; }

        [CascadingParameter ( Name = "Value2" )]
        public string Value2 { get; set; }
    }
}
