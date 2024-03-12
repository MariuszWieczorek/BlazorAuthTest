using MediatR;
using MwTech.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Recipes.Recipes.Queries.GetRecipes;

public class GetRecipesQuery : IRequest<GetRecipesViewModel>
{
    public RecipeFilter RecipeFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public int Id { get; set; }
}
