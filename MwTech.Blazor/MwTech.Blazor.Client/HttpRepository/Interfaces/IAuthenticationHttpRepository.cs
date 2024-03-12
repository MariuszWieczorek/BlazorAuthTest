using MwTech.Shared.Authentication.Commands;
using MwTech.Shared.Authentication.Dtos;
using MwTech.Shared.Common.Models;
using System.Net;

namespace MwTech.Blazor.Client.HttpRepository.Interfaces;

public interface IAuthenticationHttpRepository
{
    Task<string> RefreshToken();
    Task<ResponseDto> RegisterUser(RegisterUserCommand registerUserCommand);
    Task<HttpStatusCode> EmailConfirmation(string email, string token);
    Task<LoginUserDto> Login(LoginUserCommand userForAuthentication);
    Task Logout();
    Task<HttpStatusCode> ForgotPassword(ForgotPasswordCommand command);
    Task<ResponseDto> ResetPassword(ResetPasswordCommand command);
}
