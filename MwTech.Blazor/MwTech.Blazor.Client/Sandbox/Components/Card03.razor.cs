using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MwTech.Blazor.Client.Sandbox.Components;

public partial class Card03
{
    [Parameter]
    public string Image { get; set; }

    [Parameter]
    public RenderFragment Title { get; set; }

    [Parameter]
    public RenderFragment Content { get; set; }

    [Parameter]
    public string BtnText { get; set; }
}
