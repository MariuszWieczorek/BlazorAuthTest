using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeCategories.Commands.DeleteRecipeCategory;

public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteRecipeCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCurrencyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteRecipeCategoryCommand request, CancellationToken cancellationToken)
    {
        var RecipeCategoryToDelete = _context.RecipeCategories.Single(x => x.Id == request.Id);
        _context.RecipeCategories.Remove(RecipeCategoryToDelete);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
