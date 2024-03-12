using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Boms.Commands.DeleteBom;

public class DeleteBomCommandHandler : IRequestHandler<DeleteBomCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;

    public DeleteBomCommandHandler(IApplicationDbContext context, IProductService productWeightService)
    {
        _context = context;
        _productWeightService = productWeightService;
    }
    public async Task Handle(DeleteBomCommand request, CancellationToken cancellationToken)
    {
        var productVersionBomToDelete = _context.Boms
            .SingleOrDefault(p => p.SetId == request.ProductId && p.SetVersionId == request.ProductVersionId && p.Id == request.Id);

        _context.Boms.Remove(productVersionBomToDelete);
        
        await _context.SaveChangesAsync();

        await CalculateWeight(request);

        return;
    }

    private async Task CalculateWeight(DeleteBomCommand request)
    {
        var productVersion = _context.ProductVersions
            .Include(x => x.Product)
            .Include(x => x.Product.ProductCategory)
            .SingleOrDefault(x => x.ProductId == request.ProductId && x.Id == request.ProductVersionId);


        string productCategoryNumber = productVersion.Product.ProductCategory.CategoryNumber;

        if (productCategoryNumber == "MIE" || productCategoryNumber == "NAW")
        {
            var productQty = await _productWeightService.CalculateWeight(request.ProductId, request.ProductVersionId);
            productVersion.ProductQty = productQty;
        }

        await _context.SaveChangesAsync();
    }
}
