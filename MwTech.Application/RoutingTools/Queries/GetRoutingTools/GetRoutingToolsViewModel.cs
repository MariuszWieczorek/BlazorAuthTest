using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;
namespace MwTech.Application.RoutingTools.Queries.GetRoutingTools;

public class GetRoutingToolsViewModel
{
    public IEnumerable<RoutingTool> RoutingTools { get; set; }
    public RoutingToolFilter RoutingToolFilter { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
