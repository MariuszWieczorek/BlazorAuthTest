using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Queries.GetProducts;
using MwTech.Application.Products.Boms.Commands.AddBom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;
using MwTech.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MwTech.Application.Products.Common;
using MwTech.Application.Products.Products.Queries.GetProductsForPopup;

namespace MwTech.Application.Products.Boms.Queries.GetAddBomViewModel;

public class GetAddBomViewModelQueryHandler : IRequestHandler<GetAddBomViewModelQuery, AddBomViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAddBomViewModelQueryHandler(IApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AddBomViewModel> Handle(GetAddBomViewModelQuery request, CancellationToken cancellationToken)
    {


        var AddBomCommand = new AddBomCommand
        {
            SetId = request.ProductId,
            SetVersionId = request.ProductVersionId,
            PartId = 0,
            PartQty = 0,
            Part = new Product { Unit = new MwTech.Domain.Entities.Unit() }, 
            PartLength = 0,
            Excess = 0,
            OnProductionOrder = true,
            Layer = 0,
            OrdinalNumber = 1,
            DoNotExportToIfs = false,
            DoNotIncludeInTkw = false,
            DoNotIncludeInWeight = false
        };

        var products = _context.Products
            .Include(x => x.ProductCategory)
            .Include(x => x.Unit)
            .AsNoTracking()
            .AsQueryable();


        products = Filter(products, request.ProductFilter);

        var getProductsViewModel = new ProductsForPopupViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            ProductFilter = request.ProductFilter,
            Products = await products.Take(500).ToListAsync() // 
        };

        var vm = new AddBomViewModel
        {
            AddBomCommand = AddBomCommand,
            GetProductsForPopupViewModel = getProductsViewModel
            
        };

        return vm;

    }


    private IQueryable<Product> Filter(IQueryable<Product> products, ProductFilter productFilter)
    {
        if (productFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(productFilter.Name))
                products = products.Where(x => x.Name.Contains(productFilter.Name));

            if (!string.IsNullOrWhiteSpace(productFilter.ProductNumber))
                products = products.Where(x => x.ProductNumber.Contains(productFilter.ProductNumber));

            if (productFilter.ProductCategoryId != 0)
                products = products.Where(x => x.ProductCategoryId == productFilter.ProductCategoryId);

            if (productFilter.Id != 0)
                products = products.Where(x => x.Id == productFilter.Id);

            if (productFilter.TechCardNumber != 0 && productFilter.TechCardNumber != null)
                products = products.Where(x => x.TechCardNumber == productFilter.TechCardNumber);

            if (productFilter.IsActive == 1)
                products = products.Where(x => x.IsActive == true);

            if (!string.IsNullOrWhiteSpace(productFilter.ComponentProductNumber))
            {
                var component = _context.Products.FirstOrDefault(x => x.ProductNumber.Contains(productFilter.ComponentProductNumber));
                if (component != null)
                {
                    int componentId = component.Id;
                    products = products.Where(x => x.BomSets.Where(x => x.PartId == componentId).Any());
                }

            }
        }

        return products;
    }
}
