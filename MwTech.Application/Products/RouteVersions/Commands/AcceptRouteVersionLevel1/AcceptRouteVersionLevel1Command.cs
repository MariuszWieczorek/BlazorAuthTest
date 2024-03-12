using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.AcceptRouteVersionLevel1;

public class AcceptRouteVersionLevel1Command : IRequest
{
    public int RouteVersionId { get; set; }
    public int ProductId { get; set; }
}
