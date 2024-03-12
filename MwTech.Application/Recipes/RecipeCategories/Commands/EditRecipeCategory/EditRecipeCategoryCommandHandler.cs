using MwTech.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MwTech.Application.Recipes.RecipeCategories.Commands.EditRecipeCategory;
public class EditRecipeCategoryCommandHandler : IRequestHandler<EditRecipeCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public EditRecipeCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditRecipeCategoryCommand request, CancellationToken cancellationToken)
    {
        var RecipeCategoryToUpdate = await _context.RecipeCategories
            .SingleAsync(x => x.Id == request.Id);

        RecipeCategoryToUpdate.Name = request.Name;
        RecipeCategoryToUpdate.OrdinalNumber = request.OrdinalNumber;
        RecipeCategoryToUpdate.Description = request.Description;
        RecipeCategoryToUpdate.CategoryNumber = request.CategoryNumber;


        _context.RecipeCategories.Update(RecipeCategoryToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
