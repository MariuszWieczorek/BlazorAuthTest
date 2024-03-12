using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRecipes.Queries.ExportRecipesAltHeadersToCsv;

public class ExportRecipesAltHeadersToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedRecipeIfsVsMwTech> ComparedRecipes { get; set; }
}
