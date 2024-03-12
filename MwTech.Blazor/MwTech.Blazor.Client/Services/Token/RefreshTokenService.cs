using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MwTech.Blazor.Client.HttpRepository.Interfaces;

namespace MwTech.Blazor.Client.Services.Token;

public class RefreshTokenService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IAuthenticationHttpRepository _authenticationHttpRepository;

    public RefreshTokenService(
        AuthenticationStateProvider authenticationStateProvider,
        IAuthenticationHttpRepository authenticationHttpRepository)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _authenticationHttpRepository = authenticationHttpRepository;
    }


    // Token dostępu JWT co jakiś czas wygasa w zależności od konfiguracji.
    // Pobieramy AuthState, odczytujemy informację o użytkowniku, datę wygaśnięcia tokena
    // Jeżeli do wygaśnięcia jest mniej niż dwie minuty, to próbujemy go odświeżyć
    public async Task<string> TryRefreshToken()
    {
        

        try
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;


            // 20240226
            if (!authState.User.Identity.IsAuthenticated)
                return string.Empty;

            var expClaim = user.FindFirst(c => c.Type.Equals("exp")).Value;

            var expTime = DateTimeOffset.FromUnixTimeSeconds(
                Convert.ToInt64(expClaim));

            var now = DateTime.UtcNow;
            var diff = expTime - now;

            // Jeżeli do wygaśnięcia jest mniej niż dwie minuty, to próbujemy go odświeżyć
            await Console.Out.WriteLineAsync($"TryRefreshToken() - do wygaśnięcia zostało {diff.TotalMinutes} minut");

            if (diff.TotalMinutes <= 2)
            {
                await Console.Out.WriteLineAsync("TryRefreshToken() - Odświeżamy Token");
                return await _authenticationHttpRepository.RefreshToken();
            }
            return string.Empty;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}
