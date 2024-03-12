using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MwTech.Blazor.Client.Pages.Currencies;

public partial class CurrenciesHome
{

    static IComponentRenderMode _renderMode = new
    InteractiveAutoRenderMode(prerender: false);
}
