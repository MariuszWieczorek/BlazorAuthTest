using MwTech.Application.Common.Interfaces;
using MwTech.Application.Dictionaries;
using MediatR;

namespace MwTech.Application.Contacts.Commands.SendContactEmail;

public class SendContactEmailCommandHandler : IRequestHandler<SendContactEmailCommand>
{
    private readonly IEmail _email;
    private readonly IAppSettingsService _appSettings;

    public SendContactEmailCommandHandler(
        IEmail email,
        IAppSettingsService appSettings)
    {
        _email = email;
        _appSettings = appSettings;
    }

    public async Task Handle(SendContactEmailCommand request, CancellationToken cancellationToken)
    {
        var body = $"Nazwa: {request.Name}.<br /></br >E-mail nadawcy: {request.Email}.<br /><br />Tytuł wiadomości: {request.Title}.<br /><br />Wiadomość: {request.Message}.<br /><br />Wysłano z: GymManager.";

        await _email.SendAsync(
            $"Wiadomość z GymManager: {request.Title}",
            body,
            await _appSettings.Get(SettingsDict.AdminEmail)    
            );

        
        return;
    }
}