using FluentValidation;
using MwTech.Application.Products.ProductVersions.Commands.EditProductVersion;

namespace MwTech.Application.Common.Validators;

public class ProductVersionValidator : AbstractValidator<EditProductVersionCommand>
{
    public ProductVersionValidator()
    {
        // Check name is not null, empty and is between 1 and 250 characters
        RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 250).WithMessage("Nazwa od 5 do 250");
        

        RuleFor(x => x.AlternativeNo).NotNull().NotEmpty().WithMessage("Numer wariantu nie może być pusty.");

        RuleFor(x => x.AlternativeNo).GreaterThan(0).WithMessage("Numer wariantu musi być większy od zera.");
        RuleFor(x => x.AlternativeNo).LessThan(100).WithMessage("Numer wariantu musi być mniejszy niż 100.");

        RuleFor(x => x.VersionNumber).NotNull().NotEmpty().WithMessage("Numer wersji nie może być pusty");

        
        // Validate Age for submitted customer has to be between 21 and 100 years old
        //RuleFor(customer => customer.Age).InclusiveBetween(21, 100);

        // Validate the address (its a complex property)
        //RuleFor(customer => customer.Address).InjectValidator();
    }
}
