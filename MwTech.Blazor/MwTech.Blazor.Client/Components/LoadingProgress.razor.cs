using Microsoft.AspNetCore.Components;

namespace MwTech.Blazor.Client.Components;

public partial class LoadingProgress
{

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
