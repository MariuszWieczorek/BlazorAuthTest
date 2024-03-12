using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.DeleteProductPropertiesVersion;

public class DeleteProductPropertiesVersionCommandHandler : IRequestHandler<DeleteProductPropertiesVersionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductPropertiesVersionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteProductPropertiesVersionCommand request, CancellationToken cancellationToken)
    {

        var ProductPropertiesVersionToDelete = await _context.ProductPropertyVersions
            .SingleOrDefaultAsync(x => x.Id == request.ProductPropertiesVersionId && x.ProductId == request.ProductId);
        
        
        // usuwamy wszystkie ustawienia z usuwanej wersji ustawień
        var productPropertiesToDelete = _context.ProductProperties
                .Where(x => x.ProductPropertiesVersionId == request.ProductPropertiesVersionId);


        _context.ProductProperties.RemoveRange(productPropertiesToDelete);
                
        // Na koniec usuwamy wersje ustawień produktu

        _context.ProductPropertyVersions.Remove(ProductPropertiesVersionToDelete);
        
        await _context.SaveChangesAsync();

        return;

    }
}
