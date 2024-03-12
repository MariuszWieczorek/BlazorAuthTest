using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Queries.GetEditRouteVersionViewModel;

public class GetEditRouteVersionViewModelQuery : IRequest<EditRouteVersionViewModel>
{
    public int RouteVersionId { get; set; }
    public int ProductId { get; set; }
    public string? Tab { get; set; }
}
