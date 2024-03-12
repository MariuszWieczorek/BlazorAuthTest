using MwTech.Application.Common.Interfaces;
using MwTech.Application.Common.Models.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MwTech.Application.Users.Queries.GetUserToken;
public class GetUserTokenQueryHandler : IRequestHandler<GetUserTokenQuery, AuthenticateResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;

    public GetUserTokenQueryHandler(
        IApplicationDbContext context,
        IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    public async Task<AuthenticateResponse> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _context
            .Users
            .Select(x => new { x.Id, x.UserName, x.PasswordHash })
            .FirstOrDefaultAsync(x =>
                x.UserName == request.UserName &&
                x.PasswordHash == request.Password);

        if (user == null)
            return null;

        return _jwtService.GenerateJwtToken(user.Id);
    }
}
