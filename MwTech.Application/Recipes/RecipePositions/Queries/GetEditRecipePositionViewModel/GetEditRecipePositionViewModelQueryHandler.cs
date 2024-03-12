using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Common;
using MwTech.Application.Products.Products.Queries.GetProductsForPopup;
using MwTech.Application.Recipes.RecipePositions.Commands.EditRecipePosition;
using MwTech.Domain.Entities;
using System.Runtime.InteropServices;

namespace MwTech.Application.Recipes.RecipePositions.Queries.GetEditRecipePositionViewModel;

public class GetEditRecipePositionViewModelQueryHandler : IRequestHandler<GetEditRecipePositionViewModelQuery, EditRecipePositionViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditRecipePositionViewModelQueryHandler(
        IApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task<EditRecipePositionViewModel> Handle(GetEditRecipePositionViewModelQuery request, CancellationToken cancellationToken)
    {


        var recipePosition = await _context.RecipePositions
            .Include(x => x.Product)
            .ThenInclude(x => x.Unit)
            .Include(x => x.RecipeStage)
            .ThenInclude(x => x.RecipeVersion)
            .SingleOrDefaultAsync(x =>
                x.Id == request.RecipePositionId
             && x.RecipeStageId == request.RecipeStageId
             );


        var recipePositionsPackages = await _context.RecipePositionsPackages
            .Where(x => x.RecipeStageId == request.RecipeStageId)
            .ToListAsync();

        var editRecipePositionCommand = new EditRecipePositionCommand
        {
            Id = request.RecipePositionId,
            RecipeStageId = request.RecipeStageId,
            RecipeVersionId = recipePosition.RecipeStage.RecipeVersionId,
            RecipeId = recipePosition.RecipeStage.RecipeVersion.RecipeId,
            ProductId = recipePosition.ProductId,
            Product = recipePosition.Product,
            PositionNo = recipePosition.PositionNo,
            ProductQty = recipePosition.ProductQty,
            Description = recipePosition.Description,
            PacketNo = recipePosition.PacketNo,
            RecipePositionPackageId = recipePosition.RecipePositionPackageId,
            ReturnFromProcessing = recipePosition.ReturnFromProcessing,
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

        var vm = new EditRecipePositionViewModel
        {
            EditRecipePositionCommand = editRecipePositionCommand,
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




