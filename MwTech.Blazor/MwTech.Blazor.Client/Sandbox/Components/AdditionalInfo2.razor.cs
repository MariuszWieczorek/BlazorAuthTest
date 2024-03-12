using Microsoft.AspNetCore.Components;

namespace MwTech.Blazor.Client.Sandbox.Components;

public partial class AdditionalInfo2
{
    [CascadingParameter(Name = "Info")]
    public string Info { get; set; }

    [CascadingParameter(Name = "Title")]
    public string Title { get; set; }

}
