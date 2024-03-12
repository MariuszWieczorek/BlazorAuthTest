using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.AcceptRouteVersionLevel2;

public class AcceptRouteVersionLevel2Command : IRequest
{
    public int RouteVersionId { get; set; }
    public int ProductId { get; set; }
}
