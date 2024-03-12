using MediatR;
using MwTech.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.RoutingTools.Queries.GetRoutingTools;

public class GetRoutingToolsQuery : IRequest<GetRoutingToolsViewModel>
{
    public RoutingToolFilter RoutingToolFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public int Id { get; set; }
}
