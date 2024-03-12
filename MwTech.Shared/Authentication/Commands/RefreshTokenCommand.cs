using MediatR;
using MwTech.Shared.Authentication.Dtos;

namespace MwTech.Shared.Authentication.Commands;

public class RefreshTokenCommand : IRequest<LoginUserDto>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
