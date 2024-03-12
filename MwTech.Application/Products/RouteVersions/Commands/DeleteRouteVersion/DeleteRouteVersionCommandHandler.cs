using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Products.RouteVersions.Commands.DeleteRouteVersion;

public class DeleteRouteVersionCommandHandler : IRequestHandler<DeleteRouteVersionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRouteVersionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteRouteVersionCommand request, CancellationToken cancellationToken)
    {

        var RouteVersionToDelete = await _context.RouteVersions
            .SingleOrDefaultAsync(x => x.Id == request.RouteVersionId && x.ProductId == request.ProductId );
        
        
        // usuwamy bom
        var productBomToDelete = _context.Boms
                .Where(x => x.SetId == request.ProductId && x.SetVersionId == request.RouteVersionId);


        _context.Boms.RemoveRange(productBomToDelete);
                
        // Na koniec usuwamy wersje produktu

        _context.RouteVersions.Remove(RouteVersionToDelete);
        
        await _context.SaveChangesAsync();

        return;

    }
}
