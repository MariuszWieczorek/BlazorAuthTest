using MediatR;

namespace MwTech.Application.Recipes.Recipes.Commands.DeleteRecipe;

public class DeleteRecipeCommand : IRequest
{
    public int Id { get; set; }
}
