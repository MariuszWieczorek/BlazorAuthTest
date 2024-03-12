using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MwTech.Blazor.Client.Sandbox.Components;

public partial class Card11
{

    private string _info = "Komunikat z Card";

    [Parameter]
    public string Image { get; set; }

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public string Content { get; set; }

    [Parameter]
    public string BtnText { get; set; }
}
