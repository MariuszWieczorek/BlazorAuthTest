using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductProperties.Commands.DeleteProductProperty;

public class DeleteProductPropertyCommandHandler : IRequestHandler<DeleteProductPropertyCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductPropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteProductPropertyCommand request, CancellationToken cancellationToken)
    {
        var productPropertyToDelete = _context.ProductProperties
            .SingleOrDefault(p => p.ProductPropertiesVersionId == request.ProductPropertiesVersionId && p.Id == request.Id);
        
        _context.ProductProperties.Remove(productPropertyToDelete);
        await _context.SaveChangesAsync();

        return;
    }
}
