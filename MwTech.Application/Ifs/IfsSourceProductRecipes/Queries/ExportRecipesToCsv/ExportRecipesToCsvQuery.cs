using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRecipes.Queries.ExportRecipesToCsv;

public class ExportRecipesToCsvQuery : IRequest
{
    public IEnumerable<ComparedRecipeIfsVsMwTech> ComparedRecipes { get; set; }
}
