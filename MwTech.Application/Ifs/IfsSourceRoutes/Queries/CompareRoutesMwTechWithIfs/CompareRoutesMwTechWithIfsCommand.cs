using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;

namespace MwTech.Application.Ifs.IfsSourceRoutes.Queries.CompareRoutesMwTechWithIfs;

public class CompareRoutesMwTechWithIfsCommand : IRequest<CompareRoutesMwTechWithIfsViewModel>
{
    public CompareRouteFilter CompareRouteFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public int IdxNo { get; set; }
}
