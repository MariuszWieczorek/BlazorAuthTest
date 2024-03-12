using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Measurements;

namespace MwTech.Application.Ifs.IfsSourceRoutes.Queries.CompareRoutesIfsWithMwTech;

public class CompareRoutesIfsWithMwTechViewModel
{
    public IEnumerable<ComparedRouteIfsVsMwTech> ComparedRoutesIfsVsMwTech { get; set; }
    public CompareRouteFilter CompareRouteFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
