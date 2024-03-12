using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.Products.Queries.GetSimpleProductWithWeightByEan;

public class GetSimpleProductWithWeightByEanCommandHandler : IRequestHandler<GetSimpleProductWithWeightByEanCommand, SimpleProductWithWeight>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productService;

    public GetSimpleProductWithWeightByEanCommandHandler(IApplicationDbContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }


    public async Task<SimpleProductWithWeight> Handle(GetSimpleProductWithWeightByEanCommand request, CancellationToken cancellationToken)
    {
        Product product = null;

        if (request.Ean13Code != null)
        {
            product = await _context.Products.FirstOrDefaultAsync(x => x.Ean13Code == request.Ean13Code);
        }


        var simpleProductWithWeight = new SimpleProductWithWeight();

        if (product != null)
        {
            var weightInKg = Math.Round(await _productService.CalculateWeight(product.Id, 0),2);

            simpleProductWithWeight.ProductNumber = product.ProductNumber;
            simpleProductWithWeight.ProductId = product.Id;
            simpleProductWithWeight.Name = product.Name;
            simpleProductWithWeight.WeightInKg = weightInKg;
            simpleProductWithWeight.MinWeightInKg = Math.Round(weightInKg - weightInKg * 0.05M,2);
            simpleProductWithWeight.MaxWeightInKg = Math.Round(weightInKg + weightInKg * 0.05M,2);
            simpleProductWithWeight.Success = true;
        }
        else
        {
            simpleProductWithWeight.Success = false;
        }



        return simpleProductWithWeight;

    }
}
