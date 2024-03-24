using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L07
{
    public partial class L07_Slot_Component : ComponentBase
    {
        [Parameter]
        public RenderFragment? BasicInfo { get; set; }

        [Parameter]
        public RenderFragment? SpecialInfo { get; set; } = ( _builder ) =>
        {
            _builder.AddContent ( 0 , "<div>插槽的默认值</div>" );
            _builder.AddMarkupContent ( 1 , "<div>Hello</div>" );
        };
    }
}
