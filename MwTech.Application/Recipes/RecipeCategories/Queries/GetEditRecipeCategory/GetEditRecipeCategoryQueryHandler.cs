using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Recipes.RecipeCategories.Commands.EditRecipeCategory;

namespace MwTech.Application.Recipes.RecipeCategories.Queries.GetEditRecipeCategory;

public class GetEditRecipeCategoryQueryHandler : IRequestHandler<GetEditRecipeCategoryQuery, EditRecipeCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public GetEditRecipeCategoryQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditRecipeCategoryCommand> Handle(GetEditRecipeCategoryQuery request, CancellationToken cancellationToken)
    {

        var RecipeCategoryToEdit = _context.RecipeCategories.Single(x => x.Id == request.Id);

        return new EditRecipeCategoryCommand
        {
            Id = RecipeCategoryToEdit.Id,
            Name = RecipeCategoryToEdit.Name,
            CategoryNumber = RecipeCategoryToEdit.CategoryNumber,
            Description = RecipeCategoryToEdit.Description,
            OrdinalNumber = RecipeCategoryToEdit.OrdinalNumber,
        };
    }

}
