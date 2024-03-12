using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;

namespace MwTech.Application.Ifs.IfsSourceProductRecipes.Queries.CompareRecipesMwTechWithIfs;

public class CompareRecipesMwTechWithIfsCommand : IRequest<CompareRecipesMwTechWithIfsViewModel>
{
    public CompareRecipeFilter CompareRecipeFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
