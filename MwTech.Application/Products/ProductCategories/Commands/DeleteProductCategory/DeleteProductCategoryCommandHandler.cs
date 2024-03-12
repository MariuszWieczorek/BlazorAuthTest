using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCategories.Commands.DeleteProductCategory;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteProductCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCurrencyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategoryToDelete = _context.ProductCategories.Single(x => x.Id == request.Id);
        _context.ProductCategories.Remove(productCategoryToDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
