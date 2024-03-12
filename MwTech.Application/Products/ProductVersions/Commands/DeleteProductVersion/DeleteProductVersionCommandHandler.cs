using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Products.ProductVersions.Commands.DeleteProductVersion;

public class DeleteProductVersionCommandHandler : IRequestHandler<DeleteProductVersionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductVersionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteProductVersionCommand request, CancellationToken cancellationToken)
    {

        var productVersionToDelete = await _context.ProductVersions
            .SingleOrDefaultAsync(x => x.Id == request.ProductVersionId && x.ProductId == request.ProductId );
        
        
        // usuwamy bom
        var productBomToDelete = _context.Boms
                .Where(x => x.SetId == request.ProductId && x.SetVersionId == request.ProductVersionId);


        _context.Boms.RemoveRange(productBomToDelete);
                
        

        // Usuwamy atrybuty

        var productProperitiesToDelete = await _context.ProductVersionProperties
            .Where(x => x.ProductVersionId == request.ProductVersionId).ToListAsync();

        _context.ProductVersionProperties.RemoveRange(productProperitiesToDelete);


        // Na koniec usuwamy wersje produktu

        _context.ProductVersions.Remove(productVersionToDelete);
        
        await _context.SaveChangesAsync();

        return;

    }
}
