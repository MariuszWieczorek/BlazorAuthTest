using FluentValidation;
using MediatR;

namespace MwTech.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        int a = 1;
        // Najpierw sprawdzimy, czy mamy jakiś walidator 
        if (_validators.Any())
        {
            // pobieramy kontekst
            var context = new ValidationContext<TRequest>(request);

            // następnie walidujemy nasz kontekst
            var validationResults = await Task.WhenAll(
                _validators.Select(x =>
                 x.ValidateAsync(context, cancellationToken)));

            // sprawdzamy, czy mamy jakieś błędy
            var failures = validationResults
                .Where(x => x.Errors.Any())
                .SelectMany(x => x.Errors)
                .ToList();

            // jeżeli będą błędy, to rzucimy wyjątek, który zaraz utworzymy
            if (failures.Any())
                throw new Exceptions.ValidationException(failures);
        }

        // jeżeli jest ok to przechodzimy do kolejnego requesta
        return await next();
    }
}