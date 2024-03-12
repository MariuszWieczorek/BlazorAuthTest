using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRecipes.Queries.ExportRecipesDifferencesToExcel;

public class ExportRecipesDifferencesToExcelQuery : IRequest<string>
{
    public IEnumerable<ComparedRecipeIfsVsMwTech> ComparedRecipes { get; set; }
}
