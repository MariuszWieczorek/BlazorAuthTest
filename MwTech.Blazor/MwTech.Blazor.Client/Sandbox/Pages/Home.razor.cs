using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MwTech.Blazor.Client.Services.HttpInterceptor;

namespace MwTech.Blazor.Client.Sandbox.Pages;

public partial class Home : IDisposable
{

    static IComponentRenderMode _renderMode = new
    InteractiveAutoRenderMode(prerender: false);

    [Inject]
    public HttpInterceptorService Interceptor { get; set; }

    public void Dispose()
    {
        Interceptor.DisposeEvent();
    }
}
