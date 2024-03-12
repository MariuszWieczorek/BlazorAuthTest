using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Boms.Commands.EditBom;

public class EditBomCommandHandler : IRequestHandler<EditBomCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;

    public EditBomCommandHandler(IApplicationDbContext context, IProductService productWeightService)
    {
        _context = context;
        _productWeightService = productWeightService;
    }
    public async Task Handle(EditBomCommand request, CancellationToken cancellationToken)
    {
        var productVersionBomToEdit = _context.Boms
            .SingleOrDefault(p => p.SetId == request.SetId && p.SetVersionId == request.SetVersionId && p.Id == request.Id);

        productVersionBomToEdit.PartId = request.PartId;
        productVersionBomToEdit.OrdinalNumber = request.OrdinalNumber;
        productVersionBomToEdit.PartQty = request.PartQty;
        productVersionBomToEdit.Excess = request.Excess;
        productVersionBomToEdit.OnProductionOrder = request.OnProductionOrder;
        productVersionBomToEdit.Layer = request.Layer;
        productVersionBomToEdit.DoNotExportToIfs = request.DoNotExportToIfs;
        productVersionBomToEdit.DoNotIncludeInTkw = request.DoNotIncludeInTkw;
        productVersionBomToEdit.DoNotIncludeInWeight = request.DoNotIncludeInWeight;




        await _context.SaveChangesAsync();

        await CalculateWeight(request);

        return;
    }

    private async Task CalculateWeight(EditBomCommand request)
    {
        var productVersion = _context.ProductVersions
            .Include(x => x.Product)
            .Include(x => x.Product.ProductCategory)
            .SingleOrDefault(x => x.ProductId == request.SetId && x.Id == request.SetVersionId);


        string productCategoryNumber = productVersion.Product.ProductCategory.CategoryNumber;

        //todo dodać kategorie mie-1, mie-2 itd
        if (productCategoryNumber.StartsWith("MIE") 
            || productCategoryNumber == "NAW"
            || productCategoryNumber == "MOD"
            || productCategoryNumber == "MON"
            )
        {
            var productQty = await _productWeightService.CalculateWeight(request.SetId, request.SetVersionId);
            productVersion.ProductQty = productQty;
        }

        await _context.SaveChangesAsync();
    }
}
