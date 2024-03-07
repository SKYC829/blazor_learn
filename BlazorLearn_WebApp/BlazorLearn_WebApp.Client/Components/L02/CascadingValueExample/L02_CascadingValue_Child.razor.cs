using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    public partial class L02_CascadingValue_Child
    {
        [CascadingParameter]
        public string? ParentValue { get; set; }
    }
}
