using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Products.Queries.GetEditProductHeaderViewModel;

public class GetEditProductHeaderViewModelQueryHandler : IRequestHandler<GetEditProductHeaderViewModelQuery, EditProductHeaderViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductCostService _productCostService;

    public GetEditProductHeaderViewModelQueryHandler(IApplicationDbContext context, IProductCostService productCostService)
    {
        _context = context;
        _productCostService = productCostService;
    }

    public async Task<EditProductHeaderViewModel> Handle(GetEditProductHeaderViewModelQuery request, CancellationToken cancellationToken)
    {

        var product = _context.Products.SingleOrDefault(x => x.Id == request.ProductId);

        var productCategories = await _context.ProductCategories
            .OrderBy(x => x.OrdinalNumber)
            .AsNoTracking()
            .ToListAsync();

        var units = await _context.Units
            .OrderBy(x => x.UnitCode)
            .AsNoTracking()
            .ToListAsync();


        var editProductCommand = new EditProductCommand
        {
            Id = product.Id,
            ProductNumber = product.ProductNumber,
            OldProductNumber = product.OldProductNumber,
            TechCardNumber = product.TechCardNumber,
            Name = product.Name,
            Description = product.Description,
            ProductCategoryId = product.ProductCategoryId,
            UnitId = product.UnitId,
            ReturnedFromProd = product.ReturnedFromProd,
            NoCalculateTkw = product.NoCalculateTkw,
            IsActive = product.IsActive,
            IsTest = product.IsTest,
            MwbaseMatid = product.MwbaseMatid,
            MwbaseWyrobId = product.MwbaseWyrobId,
            Idx01 = product.Idx01,
            Idx02 = product.Idx02,
            ContentsOfRubber = product.ContentsOfRubber,
            Density = product.Density,
            ScalesId = product.ScalesId,
            Aps01 = product.Aps01,   
            Aps02 = product.Aps02,
            DecimalPlaces = product.DecimalPlaces,
            WeightTolerance = product.WeightTolerance,
        };


        var vm = new EditProductHeaderViewModel
        {
            Product = product,
            EditProductCommand = editProductCommand,
            ProductCategories = productCategories,
            Units = units,
        };
        return vm;
    }

    
}
