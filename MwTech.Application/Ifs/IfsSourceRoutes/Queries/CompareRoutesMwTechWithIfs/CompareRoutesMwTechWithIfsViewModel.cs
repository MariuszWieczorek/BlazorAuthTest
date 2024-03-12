using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Ifs.IfsSourceRoutes.Queries.CompareRoutesMwTechWithIfs;

public class CompareRoutesMwTechWithIfsViewModel
{
    public IEnumerable<ComparedRouteIfsVsMwTech> ComparedRoutesIfsVsMwTech { get; set; }
    public CompareRouteFilter CompareRouteFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
