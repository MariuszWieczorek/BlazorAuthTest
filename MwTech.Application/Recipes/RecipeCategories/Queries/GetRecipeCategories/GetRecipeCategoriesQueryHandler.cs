using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeCategories.Queries.GetRecipeCategories;

public class GetRecipeCategoriesQueryHandler : IRequestHandler<GetRecipeCategoriesQuery, RecipeCategoriesViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetRecipeCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<RecipeCategoriesViewModel> Handle(GetRecipeCategoriesQuery request, CancellationToken cancellationToken)
    {
        var RecipeCategories = _context.RecipeCategories
            .Include(x => x.Recipes)
            .OrderBy(x => x.CategoryNumber)
            .AsNoTracking()
            .AsQueryable();

        RecipeCategories = Filter(RecipeCategories, request.RecipeCategoryFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = RecipeCategories.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                RecipeCategories = RecipeCategories
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var RecipeCategoriesList = await RecipeCategories
            .OrderBy(x => x.OrdinalNumber)
            .ToListAsync();

        var vm = new RecipeCategoriesViewModel
        {
            RecipeCategories = RecipeCategoriesList,
            RecipeCategoryFilter = request.RecipeCategoryFilter,
            PagingInfo = request.PagingInfo
        };

        return vm;

    }

    public IQueryable<RecipeCategory> Filter(IQueryable<RecipeCategory> RecipeCategories, RecipeCategoryFilter RecipeCategoryFilter)
    {
        if (RecipeCategoryFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(RecipeCategoryFilter.Name))
                RecipeCategories = RecipeCategories.Where(x => x.Name.Contains(RecipeCategoryFilter.Name));

            if (!string.IsNullOrWhiteSpace(RecipeCategoryFilter.RecipeCategoryNumber))
                RecipeCategories = RecipeCategories.Where(x => x.CategoryNumber.Contains(RecipeCategoryFilter.RecipeCategoryNumber));
        }

        return RecipeCategories;
    }
}
