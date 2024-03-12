using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Measurements.Measurements.Commands.AddMeasurement;
using MwTech.Application.Products.Common;
using MwTech.Application.Products.Products.Queries.GetProductsForPopup;
using MwTech.Domain.Entities;

namespace MwTech.Application.Measurements.Measurements.Queries.GetAddMeasurementViewModel;

public class GetAddMeasurementViewModelQueryHandler : IRequestHandler<GetAddMeasurementViewModelQuery, AddMeasurementViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAddMeasurementViewModelQueryHandler(IApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AddMeasurementViewModel> Handle(GetAddMeasurementViewModelQuery request, CancellationToken cancellationToken)
    {


        var addMeasurementCommand = new AddMeasurementCommand
        {
            ProductId = request.ProductId,
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
            Products = await products.Take(500).ToListAsync() 
        };

        var vm = new AddMeasurementViewModel
        {
            AddMeasurementCommand = addMeasurementCommand,
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
        }

        return products;
    }
}
