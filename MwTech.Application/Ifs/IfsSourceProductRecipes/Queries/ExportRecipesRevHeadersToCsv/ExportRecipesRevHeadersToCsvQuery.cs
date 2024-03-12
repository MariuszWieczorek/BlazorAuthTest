using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRecipes.Queries.ExportRecipesRevHeadersToCsv;

public class ExportRecipesRevHeadersToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedRecipeIfsVsMwTech> ComparedRecipes { get; set; }
}
