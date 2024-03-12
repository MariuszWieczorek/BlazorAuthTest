using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;

namespace MwTech.Application.Ifs.IfsSourceProductRecipes.Queries.CompareRecipesIfsWithMwTech;

public class CompareRecipesIfsWithMwTechCommand : IRequest<CompareRecipesIfsWithMwTechViewModel>
{
    public CompareRecipeFilter CompareRecipeFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
