using MediatR;
using Microsoft.AspNetCore.Identity;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Shared.Authentication.Commands;
using MwTech.Shared.Authentication.Dtos;

namespace MwTech.Application.Authentication.Commands;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginUserDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthenticationService _authenticationService;

    public RefreshTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        IAuthenticationService authenticationService)
    {
        _userManager = userManager;
        _authenticationService = authenticationService;
    }

    public async Task<LoginUserDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = _authenticationService
            .GetPrincipalFromExpiredToken(request.Token);
        var name = principal.Identity.Name;

        var user = await _userManager.FindByEmailAsync(name);

        if (user == null || user.Email != name)
            return new LoginUserDto { ErrorMessage = "Nieprawidłowe żądanie." };

        if (user.RefreshToken != request.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now)
            return new LoginUserDto { ErrorMessage = "Nieprawidłowe żądanie." };

        var token = await _authenticationService.GetToken(user);
        user.RefreshToken = _authenticationService.GenerateRefreshToken();

        await _userManager.UpdateAsync(user);

        return new LoginUserDto
        {
            Token = token,
            RefreshToken = user.RefreshToken,
            IsAuthSuccessful = true
        };
    }
}
