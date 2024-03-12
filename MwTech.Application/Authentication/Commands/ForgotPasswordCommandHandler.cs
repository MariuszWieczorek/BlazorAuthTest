using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Exceptions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Shared.Authentication.Commands;

namespace MwTech.Application.Authentication.Commands;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmail _emailSender;
    private readonly IApplicationDbContext _context;

    public ForgotPasswordCommandHandler(
        UserManager<ApplicationUser> userManager,
        IEmail emailSender,
        IApplicationDbContext context)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _context = context;
    }

    public async Task Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new ValidationException("Nieprawidłowe dane.");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var param = new Dictionary<string, string>
        {
            { "token", token },
            { "email", request.Email }
        };

        var callback = QueryHelpers.AddQueryString(request.ClientURI, param);

        var body = $"<p><span style=\"font-size: 14px;\">Dzień dobry {user.Email}.</span></p><p><span style=\"font-size: 14px;\">W celu zrestowania hasła w aplikacji SimpleShop.pl kliknij w poniższy link:</span></p><p><span style=\"font-size: 14px;\"><a href='{callback}'>kliknij tutaj</a></span></p><p><span style=\"font-size: 14px;\">Pozdrawiam,</span><br /><span style=\"font-size: 14px;\">Kazimierz Szpin.</span><br /><span style=\"font-size: 14px;\">SimpleShop.pl</span>";

        await _emailSender.SendAsync(
            "Resetowanie hasła",
            body,
            user.Email);
    }
}
