using FluentValidation;
using MwTech.Application.Contacts.Commands.SendContactEmail;

namespace GymManager.Application.Contacts.Commands.SendContactEmail;
public class SendContactEmailCommandValidator : AbstractValidator<SendContactEmailCommand>
{
    //Ctrl K C Ctrl K U  
    
    //public SendContactEmailCommandValidator()
    //{
    //    RuleFor(x => x.Name)
    //        .NotEmpty().WithMessage("Pole 'Imię i Nazwisko' jest wymagane.");
    //}
}
