using MwTech.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MwTech.Application.Identity.Users.Commands.ResetPassword;
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IdentityResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserManagerService _userManagerService;

    public ResetPasswordCommandHandler(IApplicationDbContext context, IUserManagerService userManagerService)
    {
        _context = context;
        _userManagerService = userManagerService;
    }

    public async Task<IdentityResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var userToResetPassword = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        var result = new IdentityResult();

        if (userToResetPassword != null)
        {
            result = await _userManagerService.ResetPasswordAsync(request.Id);
        }

        return result;
    }
}
