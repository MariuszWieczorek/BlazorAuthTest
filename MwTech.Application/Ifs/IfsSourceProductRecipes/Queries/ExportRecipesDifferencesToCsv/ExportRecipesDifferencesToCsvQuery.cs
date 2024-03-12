using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRecipes.Queries.ExportRecipesDifferencesToCsv;

public class ExportRecipesDifferencesToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedRecipeIfsVsMwTech> ComparedRecipes { get; set; }
}
