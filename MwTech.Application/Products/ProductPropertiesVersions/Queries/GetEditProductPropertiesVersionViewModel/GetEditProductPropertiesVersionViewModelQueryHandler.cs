using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Application.Products.ProductPropertiesVersions.Commands.EditProductPropertiesVersion;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductPropertiesVersions.Queries.GetEditProductPropertiesVersionViewModel;

public class GetEditProductPropertiesVersionViewModelQueryHandler : IRequestHandler<GetEditProductPropertiesVersionViewModelQuery, EditProductPropertiesVersionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;
    private readonly IProductCostService _productCostService;

    public GetEditProductPropertiesVersionViewModelQueryHandler(
        IApplicationDbContext context,
        IProductService productWeightService,
        IProductCostService productCostService
        )
    {
        _context = context;
        _productWeightService = productWeightService;
        _productCostService = productCostService;
    }

    public async Task<EditProductPropertiesVersionViewModel> Handle(GetEditProductPropertiesVersionViewModelQuery request, CancellationToken cancellationToken)
    {

        var ProductPropertiesVersion = await _context.ProductPropertyVersions
            .FirstOrDefaultAsync(x => x.Id == request.ProductPropertiesVersionId);

 
        var ProductProperties = await GetProductProperties(request);

        var editProductPropertiesVersionCommand = new EditProductPropertiesVersionCommand
        {
            Id = request.ProductPropertiesVersionId,
            ProductId = request.ProductId,
            Product = await _context.Products.SingleOrDefaultAsync(x=>x.Id == request.ProductId),
            VersionNumber = ProductPropertiesVersion.VersionNumber,
            AlternativeNo = ProductPropertiesVersion.AlternativeNo,
            Name = ProductPropertiesVersion.Name,
            Description = ProductPropertiesVersion.Description,
            IsActive = ProductPropertiesVersion.IsActive,
            ModifiedByUserId = ProductPropertiesVersion.ModifiedByUserId,
            ModifiedDate = ProductPropertiesVersion.ModifiedDate,
            IsAccepted01 = ProductPropertiesVersion.IsAccepted01,
            IsAccepted02 = ProductPropertiesVersion.IsAccepted02,
            Tab = request.Tab,
            ReturnAddress = request.ReturnAddress,
            Anchor = request.Anchor
        };


        var vm = new EditProductPropertiesVersionViewModel
        {
            EditProductPropertiesVersionCommand = editProductPropertiesVersionCommand,
            ProductProperties = ProductProperties
        };
        return vm;
    }

    private async Task<List<ProductProperty>> GetProductProperties(GetEditProductPropertiesVersionViewModelQuery request)
    {
        

        var productProperties = await _context.ProductProperties
            .Include(x=>x.ProductPropertiesVersion)
            .ThenInclude(x => x.Product)
            .Include(x => x.Property)
            .ThenInclude(x => x.Unit)
            .Include(x => x.Property)
            .ThenInclude(x => x.ProductCategory)
            .Where(x => x.ProductPropertiesVersionId == request.ProductPropertiesVersionId)
            .OrderBy(x => x.Property.ProductCategory.OrdinalNumber)
            .ThenBy(x => x.Property.PropertyNumber)
            .AsNoTracking()
            .ToListAsync();

        return productProperties;
    }


}




