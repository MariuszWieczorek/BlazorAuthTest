using MwTech.Application.Common.Models.Auth;

namespace MwTech.Application.Common.Interfaces;
public interface IJwtService
{
    AuthenticateResponse GenerateJwtToken(string userId);
}
