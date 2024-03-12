using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using MwTech.Blazor.Client.HttpRepository.Interfaces;
using MwTech.Blazor.Client.Services.AuthStateProviders;
using MwTech.Shared.Authentication.Commands;
using MwTech.Shared.Authentication.Dtos;
using MwTech.Shared.Common.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MwTech.Blazor.Client.HttpRepository;

public class AuthenticationHttpRepository : IAuthenticationHttpRepository
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;
    private readonly NavigationManager _navManager;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly JsonSerializerOptions _options =
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public AuthenticationHttpRepository(
        HttpClient client,
        ILocalStorageService localStorage,
        NavigationManager navManager,
        AuthenticationStateProvider authStateProvider
        )
    {
        _client = client;
        _localStorage = localStorage;
        _navManager = navManager;
        _authStateProvider = authStateProvider;
    }

    // pobieramy oba tokeny z Local Storage
    // jeżeli Refreshtoken jeszcze nie wygasł, to chcemy odświeżyć token dostępu.
    public async Task<string> RefreshToken()
    {
        
        var token = await _localStorage.GetItemAsync<string>("authToken");
        var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

        // wywołujemy endpointa, który odświeża tokena
        var response = await _client.PostAsJsonAsync("v1/token/refresh",
            new RefreshTokenCommand
            {
                Token = token,
                RefreshToken = refreshToken
            });

        // odczytujemy zawartość, która została zwrócona
        var content = await response.Content.ReadAsStringAsync();

        // deserializujemy na LoginUserDto
        var result = JsonSerializer.Deserialize<LoginUserDto>(content, _options);

        if (!result.IsAuthSuccessful)
            return null;

        // zapamiętujemy nowe tokeny w LocalStorage
        await _localStorage.SetItemAsync("authToken", result.Token);
        await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

        // aktualizujemy nagłówek HttpClient
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
            ("bearer", result.Token);

        // zwracamy odświeżony token.
        await Console.Out.WriteLineAsync("odświeżony token \n" + result.Token);
        return result.Token;
    }

    public async Task<ResponseDto> RegisterUser(RegisterUserCommand registerUserCommand)
    {
        registerUserCommand.ClientURI = Path.Combine(
            _navManager.BaseUri, "potwierdzenie-email");

        var response = await _client.PostAsJsonAsync("v1/account/register",
            registerUserCommand);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ResponseDto>(content, _options);

            return result;
        }

        return new ResponseDto { IsSuccess = true };
    }

    public async Task<HttpStatusCode> EmailConfirmation(string email, string token)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["email"] = email,
            ["token"] = token
        };

        var response = await _client.GetAsync(QueryHelpers.AddQueryString(
            "v1/account/emailconfirmation", queryStringParam));

        return response.StatusCode;
    }

    public async Task<LoginUserDto> Login(LoginUserCommand userForAuthentication)
    {
        var response = await _client.PostAsJsonAsync("v1/account/login",
            userForAuthentication);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<LoginUserDto>(content, _options);

        if (!response.IsSuccessStatusCode)
            return result;

        await _localStorage.SetItemAsync("authToken", result.Token);
        await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(
            result.Token);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "bearer", result.Token);

        return new LoginUserDto { IsAuthSuccessful = true };
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("refreshToken");

        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

        _client.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<HttpStatusCode> ForgotPassword(ForgotPasswordCommand command)
    {
        command.ClientURI =
            Path.Combine(_navManager.BaseUri, "reset-hasla");

        var result = await _client.PostAsJsonAsync("v1/account/forgotpassword",
            command);

        return result.StatusCode;
    }

    public async Task<ResponseDto> ResetPassword(ResetPasswordCommand command)
    {
        var resetResult = await _client.PostAsJsonAsync("v1/account/resetpassword",
            command);

        var resetContent = await resetResult.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<ResponseDto>(resetContent,
            _options);

        return result;
    }
}
