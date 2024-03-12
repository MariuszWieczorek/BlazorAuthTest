using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.AddProduct;
using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Products.Queries.GetAddProductViewModel;

public class GetAddProductViewModelQueryHandler : IRequestHandler<GetAddProductViewModelQuery, AddProductViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddProductViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddProductViewModel> Handle(GetAddProductViewModelQuery request, CancellationToken cancellationToken)
    {

        var product = new Product
        {

        };

        var productCategories = await _context.ProductCategories
            .OrderBy(x => x.OrdinalNumber)
            .AsNoTracking()
            .ToListAsync();

        var units = await _context.Units
            .OrderBy(x => x.UnitCode)
            .AsNoTracking()
            .ToListAsync();

        IEnumerable<ProductVersion> productVersions = null;
        IEnumerable<ProductSettingVersion> productSettingsVersions = null;
        IEnumerable<RouteVersion> routeVersions = null;
        IEnumerable<ProductProperty> productProperties = null;


        var addProductCommand = new AddProductCommand
        {
            ProductNumber = "",
            OldProductNumber = "",
            TechCardNumber = null,
            Name = "",
            Description = null,
            ProductCategoryId = 0,
            UnitId = 0,
            ReturnedFromProd = false,
            NoCalculateTkw = false,
            IsActive = true,
            IsTest = false,
            MwbaseMatid = 0,
            MwbaseWyrobId = 0
        };


        var vm = new AddProductViewModel
        {
            AddProductCommand = addProductCommand,
            ProductCategories = productCategories,
            Units = units,
        };
        return vm;
    }
}
