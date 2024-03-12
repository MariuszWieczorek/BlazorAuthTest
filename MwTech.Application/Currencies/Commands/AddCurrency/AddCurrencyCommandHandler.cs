using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Shared.Currencies.Commands.AddCurrency;
using Unit = MediatR.Unit;

namespace MwTech.Application.Currencies.Commands.AddCurrency;

public class AddCurrencyCommandHandler : IRequestHandler<AddCurrencyCommand>
{
    private readonly IApplicationDbContext _context;

    public AddCurrencyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currency = new Currency();
        currency.CurrencyCode = request.CurrencyCode;
        currency.Name = request.Name;

        await _context.Currencies.AddAsync(currency);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
