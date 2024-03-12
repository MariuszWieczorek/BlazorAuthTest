using Microsoft.AspNetCore.Components;

namespace MwTech.Blazor.Client.Sandbox.Components;

public partial class AdditionalInfo1
{
    [CascadingParameter]
    public string Info { get; set; }

}
