using MediatR;
using MwTech.Domain.Entities.Recipes;

namespace MwTech.Application.Recipes.Recipes.Queries.ExportRecipesToExcel;

public class ExportRecipesToExcelQuery : IRequest<string>
{
    public IEnumerable<Recipe> Recipes { get; set; }
}
