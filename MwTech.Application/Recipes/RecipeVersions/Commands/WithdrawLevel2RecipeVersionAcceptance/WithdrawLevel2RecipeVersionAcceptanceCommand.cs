using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.WithdrawLevel2RecipeVersionAcceptance;

public class WithdrawLevel2RecipeVersionAcceptanceCommand : IRequest
{
    public int RecipeVersionId { get; set; }
    public int RecipeId { get; set; }
}
