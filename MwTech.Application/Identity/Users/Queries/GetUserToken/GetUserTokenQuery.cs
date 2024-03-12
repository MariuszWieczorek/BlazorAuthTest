using MwTech.Application.Common.Models.Auth;
using MediatR;


namespace MwTech.Application.Users.Queries.GetUserToken;
public class GetUserTokenQuery : IRequest<AuthenticateResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
