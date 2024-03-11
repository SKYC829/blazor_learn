using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    public partial class L02_Multiple_CascadingValue_Parent_1 : ComponentBase
    {
        [CascadingParameter]
        internal CascadingObject Value { get; set; }
    }
}
