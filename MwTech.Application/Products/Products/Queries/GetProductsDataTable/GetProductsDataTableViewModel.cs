using MwTech.Shared.Common.Models;
using MwTech.Domain.Entities;
using MwTech.Application.Common.Models;
namespace MwTech.Application.Products.Products.Queries.GetProductsDataTable;

public class GetProductsDataTableViewModel
{
    public ProductDataTableFilter ProductFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    public PaginatedList<ProductDataTableDto> Products { get; set; }

}
