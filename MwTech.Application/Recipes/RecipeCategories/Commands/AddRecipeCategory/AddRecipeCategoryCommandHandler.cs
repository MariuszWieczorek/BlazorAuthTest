using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.RecipeCategories.Commands.AddRecipeCategory;

public class AddRecipeCategoryCommandHandler : IRequestHandler<AddRecipeCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public AddRecipeCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddRecipeCategoryCommand request, CancellationToken cancellationToken)
    {
        var RecipeCategory = new RecipeCategory();


        RecipeCategory.OrdinalNumber = request.OrdinalNumber;
        RecipeCategory.Name = request.Name;
        RecipeCategory.CategoryNumber = request.CategoryNumber;
        RecipeCategory.Description = request.Description;

        await _context.RecipeCategories.AddAsync(RecipeCategory);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
