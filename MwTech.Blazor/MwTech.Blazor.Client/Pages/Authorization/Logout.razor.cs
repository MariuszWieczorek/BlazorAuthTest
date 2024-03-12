using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MwTech.Blazor.Client.HttpRepository.Interfaces;

namespace MwTech.Blazor.Client.Pages.Authorization;

public partial class Logout
{
    static IComponentRenderMode _renderMode = new InteractiveAutoRenderMode(prerender: false);

    [Inject]
    public IAuthenticationHttpRepository AuthenticationHttpRepository { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await AuthenticationHttpRepository.Logout();
        NavigationManager.NavigateTo("/logowanie");
    }
}
