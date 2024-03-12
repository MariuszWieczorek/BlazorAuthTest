using MediatR;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.WithdrawLevel1RecipeVersionAcceptance;

public class WithdrawLevel1RecipeVersionAcceptanceCommand : IRequest
{
    public int RecipeVersionId { get; set; }
    public int RecipeId { get; set; }
}
