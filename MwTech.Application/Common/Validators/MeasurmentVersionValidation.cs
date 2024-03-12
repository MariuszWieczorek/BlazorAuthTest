using FluentValidation;
using MwTech.Application.Measurements.Measurements.Commands.AddMeasurement;
using MwTech.Application.Products.ProductVersions.Commands.EditProductVersion;

namespace MwTech.Application.Common.Validators;



public class AddMeasurementValidator : AbstractValidator<AddMeasurementCommand>
{
    public AddMeasurementValidator()
    {
        // Check name is not null, empty and is between 1 and 250 characters
        RuleFor(x => x.ProductId).NotNull().NotEmpty().WithMessage("Produkt nie może być pusty.");
        RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Nie wybrano produktu.");


        RuleFor(x => x.Value).NotNull().NotEmpty().WithMessage("Wartość zważona nie może być pusta.");
        RuleFor(x => x.Value).GreaterThan(0).WithMessage("Wartość zważona musi być większa od zera.");


        
        // Validate Age for submitted customer has to be between 21 and 100 years old
        //RuleFor(customer => customer.Age).InclusiveBetween(21, 100);

        // Validate the address (its a complex property)
        //RuleFor(customer => customer.Address).InjectValidator();
    }
}


