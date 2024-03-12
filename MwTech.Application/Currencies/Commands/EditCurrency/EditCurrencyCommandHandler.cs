using MwTech.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Shared.Currencies.Commands.EditCurrency;

namespace MwTech.Application.Currencies.Commands.EditCurrency;
public class EditCurrencyCommandHandler : IRequestHandler<EditCurrencyCommand>
{
    private readonly IApplicationDbContext _context;

    public EditCurrencyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currencyToUpdate = await _context.Currencies
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        currencyToUpdate.Name = request.Name;
        currencyToUpdate.CurrencyCode = request.CurrencyCode;


        _context.Currencies.Update(currencyToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
