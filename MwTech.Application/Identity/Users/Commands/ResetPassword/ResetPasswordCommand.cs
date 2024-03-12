using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MwTech.Application.Identity.Users.Commands.ResetPassword;
public class ResetPasswordCommand : IRequest<IdentityResult>
{
    public string Id { get; set; }
}
