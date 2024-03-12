using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Products.ProductCosts.Commands.DeleteProductCost;

public class DeleteProductCostCommandHandler : IRequestHandler<DeleteProductCostCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCostCommand request, CancellationToken cancellationToken)
    {
        
        var productCostToDelete = await _context.ProductCosts.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.ProductCosts.Remove(productCostToDelete);
        await _context.SaveChangesAsync();

        return;

    }
}
