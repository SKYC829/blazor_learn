using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    public partial class L02_Multiple_CascadingValue : ComponentBase
    {
        public string Value1 { get; } = "Hello";

        public string Value2 { get; } = "World";

        internal CascadingObject CascadingObject => new ();
    }
}
