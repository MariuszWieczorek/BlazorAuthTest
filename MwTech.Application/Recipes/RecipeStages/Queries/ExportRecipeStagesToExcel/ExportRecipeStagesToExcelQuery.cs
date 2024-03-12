using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Recipes.RecipeStages.Queries.ExportRecipeStagesToExcel;

public class ExportRecipeStagesToExcelQuery : IRequest<string>
{
    public int RecipeVersionId { get; set; }
}
