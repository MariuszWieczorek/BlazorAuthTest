using MediatR;
using MwTech.Application.Operations.Queries.GetOperations;
using MwTech.Application.Products.Products.Queries.GetProducts;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Application.RoutingTools.Queries.GetRoutingTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Routes.Queries.GetAddRouteViewModel;

public class GetAddRouteViewModelQuery : IRequest<AddRouteViewModel>
{
    public int RouteVersionId { get; set; }
    public int ProductId { get; set; }
    public ResourceFilter ResourceFilter { get; set; }
    public OperationFilter OperationFilter { get; set; }
    public RoutingToolFilter RoutingToolFilter { get; set; }
    public string Tab { get; set; }

}
