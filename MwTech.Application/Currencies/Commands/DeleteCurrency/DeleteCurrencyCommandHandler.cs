using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Shared.Currencies.Commands.DeleteCurrency;

namespace MwTech.Application.Currencies.Commands.DeleteCurrency;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCurrencyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        var currencyToDelete = _context.Currencies.Single(x => x.Id == request.Id);
        _context.Currencies.Remove(currencyToDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
