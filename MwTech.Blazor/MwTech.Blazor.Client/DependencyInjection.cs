using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using MwTech.Blazor.Client.HttpRepository;
using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Services;
using Radzen;
using MwTech.Blazor.Client.Services.HttpInterceptor;
using MwTech.Blazor.Client.Services.Token;
using MwTech.Blazor.Client.Services.AuthStateProviders;

namespace MwTech.Blazor.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClient(this IServiceCollection services, Uri uri)
    {
        #region konfiguracja klienta Http i Interceptora

        // dodając Interceptora oprócz HttpClient przekazujemy jeszcze IServiceProvider
        
        
        services.AddHttpClient("MWTechAPI", (sp, client) =>
        {
            client.BaseAddress = uri;
            client.Timeout = TimeSpan.FromMinutes(5);
            client.EnableIntercept(sp);
        });

        services.AddScoped(sp =>
            sp.GetService<IHttpClientFactory>().CreateClient("MWTechAPI"));


        // serwisy dodane w związku z Interceptorem 
        services.AddHttpClientInterceptor();
        services.AddScoped<HttpInterceptorService>();

        #endregion 


        services.AddScoped<ICurrencyHttpRepository, CurrencyHttpRepository>();
        services.AddScoped<ITyreHttpRepository, TyreHttpRepository>();

        services.AddBlazoredLocalStorage();


        // użycie js
        services.AddScoped<ToastrService>();

        // Autoryzacja 
        services.AddOptions();
        services.AddAuthorizationCore();


        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

        services.AddCascadingAuthenticationState();
        services.AddScoped<IAuthenticationHttpRepository, AuthenticationHttpRepository>();
        services.AddScoped<RefreshTokenService>();
        
        services.AddRadzenComponents();
       // services.AddTelerikBlazor();

        return services;
    }
}
