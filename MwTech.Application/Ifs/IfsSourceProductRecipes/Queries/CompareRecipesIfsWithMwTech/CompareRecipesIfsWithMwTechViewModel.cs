using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Ifs.IfsSourceProductRecipes.Queries.CompareRecipesIfsWithMwTech;

public class CompareRecipesIfsWithMwTechViewModel
{
    public IEnumerable<ComparedRecipeIfsVsMwTech> ComparedRecipesIfsVsMwTech { get; set; }
    public CompareRecipeFilter CompareRecipeFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
