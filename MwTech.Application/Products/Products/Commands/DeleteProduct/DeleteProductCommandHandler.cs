using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Products.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        // KOGT1834080121L

        var productToDelete = await _context.Products
            .SingleOrDefaultAsync(x => x.Id == request.Id);


        // Usuwamy wersje struktur produktowych i ich bomy
        await DeleteBoms(request);


        // Usuwamy wersje rutingu i listy czynności
        await DeleteRoutes(request);


        // Usuwamy koszty powiązane z produktem
        await DeleteProductCosts(request);


        // Usuwamy Ustawienia Maszyn
        await DeleteSettings(request);


        // Usuwamy Atrybuty
        await DeleteProperties(request);
        // Usuwamy Produkt

        _context.Products.Remove(productToDelete);

        await _context.SaveChangesAsync();

        return;

    }
    private async Task DeleteProductCosts(DeleteProductCommand request)
    {
        var productCostsToDelete = await _context.ProductCosts
                    .Where(x => x.ProductId == request.Id).ToListAsync();

        _context.ProductCosts.RemoveRange(productCostsToDelete);

        // Usuwamy atrybuty

        var productProperitiesToDelete = await _context.ProductProperties
            .Include(x=>x.ProductPropertiesVersion)
            .ThenInclude(x=>x.Product)
            .Where(x => x.ProductPropertiesVersion.ProductId == request.Id).ToListAsync();

        _context.ProductProperties.RemoveRange(productProperitiesToDelete);
    }
    private async Task DeleteRoutes(DeleteProductCommand request)
    {
        var productRouteVersionsToDelete = await _context.RouteVersions
            .Where(x => x.ProductId == request.Id).ToListAsync();


        foreach (var item in productRouteVersionsToDelete)
        {
            var routesToDelete = await _context.ManufactoringRoutes
                .Where(x => x.RouteVersionId == item.Id).ToListAsync();

            _context.ManufactoringRoutes.RemoveRange(routesToDelete);
        }

        _context.RouteVersions.RemoveRange(productRouteVersionsToDelete);
    }
    private async Task DeleteBoms(DeleteProductCommand request)
    {
        var productVersionsToDelete = await _context.ProductVersions
                    .Where(x => x.ProductId == request.Id).ToListAsync();

        var productBomsToDelete = _context.Boms
                .Where(x => x.SetId == request.Id);


        _context.Boms.RemoveRange(productBomsToDelete);

        _context.ProductVersions.RemoveRange(productVersionsToDelete);
    }
    private async Task DeleteSettings(DeleteProductCommand request)
    {
        var productSettingVersionsToDelete = await _context.ProductSettingVersions
                    .Where(x => x.ProductId == request.Id).ToListAsync();

        var productSettingsToDelete = _context.ProductSettingVersionPositions
                .Include(x=>x.ProductSettingVersion)
                .Where(x => x.ProductSettingVersion.ProductId == request.Id);


        _context.ProductSettingVersionPositions.RemoveRange(productSettingsToDelete);

        _context.ProductSettingVersions.RemoveRange(productSettingVersionsToDelete);
    }
    private async Task DeleteProperties(DeleteProductCommand request)
    {
        var productPropertiesVersionsToDelete = await _context.ProductPropertyVersions
                    .Where(x => x.ProductId == request.Id).ToListAsync();

        var productPropertiesToDelete = _context.ProductProperties
                .Include(x => x.ProductPropertiesVersion)
                .Where(x => x.ProductPropertiesVersion.ProductId == request.Id);


        _context.ProductProperties.RemoveRange(productPropertiesToDelete);

        _context.ProductPropertyVersions.RemoveRange(productPropertiesVersionsToDelete);
    }
}
