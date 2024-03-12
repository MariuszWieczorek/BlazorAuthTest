using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MwTech.Blazor.Components;

public partial class App
{
    static IComponentRenderMode _renderMode = new
    InteractiveAutoRenderMode(prerender: false);

}
