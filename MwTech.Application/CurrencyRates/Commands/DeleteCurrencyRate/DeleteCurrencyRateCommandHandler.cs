using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.CurrencyRates.Commands.DeleteCurrencyRate;

public class DeleteCurrencyRateCommandHandler : IRequestHandler<DeleteCurrencyRateCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCurrencyRateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCurrencyRateCommand request, CancellationToken cancellationToken)
    {
        
        var currencyRateToDelete = await _context.CurrencyRates.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.CurrencyRates.Remove(currencyRateToDelete);
        await _context.SaveChangesAsync();

        return;

    }
}
