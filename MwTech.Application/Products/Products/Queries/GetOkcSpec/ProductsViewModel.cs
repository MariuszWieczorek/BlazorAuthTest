using MwTech.Application.Common.Models;
using MwTech.Application.Products.Common;
using MwTech.Domain.Entities;
namespace MwTech.Application.Products.Products.Queries.GetOkcSpec;

public class OkcSpecViewModel
{
    public OkcFilter OkcFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    public List<OkcSpecDto> Products { get; set; }
    public int ProductsCount { get; set; }

}
