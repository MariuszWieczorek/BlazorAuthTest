using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Exceptions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Shared.Authentication.Commands;


namespace MwTech.Application.Authentication.Commands;

public class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;

    public EmailConfirmationCommandHandler(
        UserManager<ApplicationUser> userManager,
        IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new ValidationException();

        if (user.EmailConfirmed)
            return;

        var confirmResult = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (!confirmResult.Succeeded)
            throw new ValidationException();
    }
}
