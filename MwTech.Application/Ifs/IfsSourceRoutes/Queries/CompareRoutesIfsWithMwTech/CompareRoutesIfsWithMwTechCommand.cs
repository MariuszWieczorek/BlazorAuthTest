using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsSourceRoutes.Queries.CompareRoutesIfsWithMwTech;

public class CompareRoutesIfsWithMwTechCommand : IRequest<CompareRoutesIfsWithMwTechViewModel>
{
    public CompareRouteFilter CompareRouteFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public int IdxNo { get; set; }
}
