using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MwTech.Blazor.Client.Services.Token;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MwTech.Blazor.Client.AuthStateProviders;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public AuthStateProvider(
        HttpClient httpClient,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Zwracamy informację o tym, czy użytkownik jest zalogowany, czy nie.
        // Informacje o tokenach JWT przechowujemy po stronie przeglądarki w LocalStorage.
        // Jeżeli nie mamy tokena, to zwracamy informację, że użytkownik jest niezalogowany.
        // Jeżeli mamy token, to będziemy ustawiać go w headerze HttpClient’a
        // oraz pobierzemy sobie z niego Claimsy, czyli informacje o zalogowanym użytkowniku. 
        // Następnie zwracamy AuthState wraz z pobranymi za pomocą naszego  parsera claimsami.

        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(token))
            return _anonymous;

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", token);

        //return await Task.FromResult(FakeAuthState());

        var authState = new 
            AuthenticationState(new 
                ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")
                ));
        return authState;
    }

    private AuthenticationState FakeAuthState()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "k.szpin@wp.pl"),
            new Claim(ClaimTypes.Role, "Administrator")
        };

        return new AuthenticationState(new
            ClaimsPrincipal(new ClaimsIdentity(claims, "test")));
    }

    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));

        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);

        NotifyAuthenticationStateChanged(authState);
    }
}
