using Microsoft.AspNetCore.Components;
using MwTech.Blazor.Client.Services.Token;
using System.Net;
using System.Net.Http.Headers;
using Toolbelt.Blazor;

namespace MwTech.Blazor.Client.Services.HttpInterceptor;

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly NavigationManager _navigationManager;
    private readonly RefreshTokenService _refreshTokenService;
    private readonly NavigationManager _navManager;

    public HttpInterceptorService(
        HttpClientInterceptor interceptor,
        NavigationManager navigationManager,
        RefreshTokenService refreshTokenService,
        NavigationManager navManager)
    {
        _interceptor = interceptor;
        _navigationManager = navigationManager;
        _refreshTokenService = refreshTokenService;
        _navManager = navManager;
    }



    public async Task RegisterEvent()
    {
        _interceptor.AfterSendAsync += HandleResponse;
         await Task.Delay(100);
    }
    // Musimy dać możliwość zarejestrowania tych metod,
    // będziemy chcieli podpiąć się do interceptora do metody BeforeSendAsync()
    // i dodać wywołanie InterceptorBeforeSendAsync()
    public async Task  RegisterBeforeSendEvent()
    {
        _interceptor.BeforeSendAsync += InterceptBeforeSendAsync;
        await Task.Delay(100);
    }

    public void DisposeEvent()
    {
        _interceptor.AfterSendAsync -= HandleResponse;
        _interceptor.BeforeSendAsync -= InterceptBeforeSendAsync;
    }

    // będziemy chcieli podpiąć się pod zdarzenie wykonywane przed requestem.
    private async Task InterceptBeforeSendAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        // Sprawdzamy ścieżkę, która została wywołana


        var absolutePath = e.Request.RequestUri.AbsolutePath;

        // Jeżeli nie jest to ścieżka, która zawiera słowa token lub account,
        // czyli nie wywołujemy metod z kontrolerów o tych nazwach, to wywołujemy metodę TryRefreshToken.

        if (!absolutePath.Contains("token") && !absolutePath.Contains("account"))
        {
            var token = await _refreshTokenService.TryRefreshToken();

            // Jeżeli token został odświeżony to musimy w tym requeście i dodać do Headera nowy token.
            if (!string.IsNullOrEmpty(token))
            {
                e.Request.Headers.Authorization =
                    new AuthenticationHeaderValue(" ", token);
            }
        }
    }

    private async Task HandleResponse(object sender, HttpClientInterceptorEventArgs e)
    {
        var message = string.Empty;

        if (e.Response == null)
        {
                _navigationManager.NavigateTo("/customerror");
            message = "Interceptor - Responce == NULL.";
            throw new HttpResponseException(message);
        }



        if (!e.Response.IsSuccessStatusCode)
        {
            switch (e.Response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    _navigationManager.NavigateTo("/404");
                    message = "Nie znaleziono zasobu.";
                    break;
                case HttpStatusCode.Unauthorized:
                    _navManager.NavigateTo("/logowanie");
                    message = "Dostęp zabroniony";
                    break;
                default:
                    _navigationManager.NavigateTo("/customerror");
                    message = "Coś poszło nie tak, proszę skontaktuj się z administratorem.";
                    break;
            }

            throw new HttpResponseException(message);
        }
        else
        {
            int i = 1;
        }

    }
}
