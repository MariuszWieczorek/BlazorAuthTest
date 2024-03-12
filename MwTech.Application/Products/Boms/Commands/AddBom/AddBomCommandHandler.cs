using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Boms.Commands.AddBom;

public class AddBomCommandHandler : IRequestHandler<AddBomCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;

    public AddBomCommandHandler(IApplicationDbContext context, IProductService productWeightService)
    {
        _context = context;
        _productWeightService = productWeightService;
    }
    public async Task Handle(AddBomCommand request, CancellationToken cancellationToken)
    {
        var productVersionBomToAdd = new Bom
        {
            SetId = request.SetId,
            SetVersionId = request.SetVersionId,
            PartId = request.PartId,
            OrdinalNumber = request.OrdinalNumber,
            PartQty = request.PartQty,
            Part = request.Part,
            Excess = request.Excess,
            OnProductionOrder = request.OnProductionOrder,
            Layer = request.Layer,
            DoNotExportToIfs= request.DoNotExportToIfs,
            DoNotIncludeInTkw= request.DoNotIncludeInTkw,
            DoNotIncludeInWeight= request.DoNotIncludeInWeight
        };

        _context.Boms.Add(productVersionBomToAdd);

        var productVersion = _context.ProductVersions
           .Include(x => x.Product)
           .Include(x => x.Product.ProductCategory)
           .SingleOrDefault(x => x.ProductId == request.SetId && x.Id == request.SetVersionId);


        string productCategoryNumber = productVersion.Product.ProductCategory.CategoryNumber;

        if (productCategoryNumber == "MIE" || productCategoryNumber == "NAW")
        {
            var productQty = await _productWeightService.CalculateWeight(request.SetId, request.SetVersionId);
            productVersion.ProductQty = productQty;
        }

        await _context.SaveChangesAsync();

        await CalculateWeight(request);

        return;
    }

    private async Task CalculateWeight(AddBomCommand request)
    {
        var productVersion = _context.ProductVersions
            .Include(x => x.Product)
            .Include(x => x.Product.ProductCategory)
            .SingleOrDefault(x => x.ProductId == request.SetId && x.Id == request.SetVersionId);


        string productCategoryNumber = productVersion.Product.ProductCategory.CategoryNumber;

        if (productCategoryNumber == "MIE" || productCategoryNumber == "NAW")
        {
            var productQty = await _productWeightService.CalculateWeight(request.SetId, request.SetVersionId);
            productVersion.ProductQty = productQty;
        }

        await _context.SaveChangesAsync();
    }
}
