using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;
namespace MwTech.Application.Resources.Queries.GetResources;

public class GetResourcesViewModel
{
    public IEnumerable<Resource> Resources { get; set; }
    public ResourceFilter ResourceFilter { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
