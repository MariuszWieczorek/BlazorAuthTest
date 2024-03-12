using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Common;
using MwTech.Application.Products.Products.Queries.GetProductsForPopup;
using MwTech.Application.Recipes.RecipePositions.Commands.AddRecipePosition;
using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipePositions.Queries.GetAddRecipePositionViewModel;

public class GetAddRecipePositionViewModelQueryHandler : IRequestHandler<GetAddRecipePositionViewModelQuery, AddRecipePositionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public GetAddRecipePositionViewModelQueryHandler(IApplicationDbContext context,
          ICurrentUserService currentUserService,
          IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<AddRecipePositionViewModel> Handle(GetAddRecipePositionViewModelQuery request, CancellationToken cancellationToken)
    {

        int newRecipePositionNumber = 0;

        var numberOfPositions = _context.RecipePositions
            .Where(x => x.RecipeStageId == request.RecipeStageId)
            .Count();

        var recipeStage = await _context.RecipeStages
            .Include(x => x.RecipeVersion)
            .SingleOrDefaultAsync(x =>
               x.Id == request.RecipeStageId
             );

        if (numberOfPositions != 0)
        {
            newRecipePositionNumber = _context.RecipePositions
                .Where(x => x.RecipeStageId == request.RecipeStageId)
                .Max(x => x.PositionNo) + 1;
        }
        else
        {
            newRecipePositionNumber = recipeStage.StageNo > 1 ? 2 : 1;
        }






        var AddRecipePositionCommand = new AddRecipePositionCommand
        {
            RecipeStageId = request.RecipeStageId,
            RecipeVersionId = recipeStage.RecipeVersionId,
            RecipeId = recipeStage.RecipeVersion.RecipeId,
            PositionNo = newRecipePositionNumber,
            Product = new Product { Unit = new Domain.Entities.Unit() },
            Description = ""
        };

        var products = _context.Products
            .Include(x => x.ProductCategory)
            .Include(x => x.Unit)
            .AsNoTracking()
            .AsQueryable();



        products = Filter(products, request.ProductFilter);


        var productsForPopupViewModel = new ProductsForPopupViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            ProductFilter = request.ProductFilter,
            Products = await products.Take(500).ToListAsync() // 
        };

        var recipePositionsPackages = await _context.RecipePositionsPackages
            .Where(x => x.RecipeStageId == request.RecipeStageId)
            .ToListAsync();

        var vm = new AddRecipePositionViewModel
        {
            AddRecipePositionCommand = AddRecipePositionCommand,
            GetProductsForPopupViewModel = productsForPopupViewModel,
            RecipePositionsPackages = recipePositionsPackages
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
