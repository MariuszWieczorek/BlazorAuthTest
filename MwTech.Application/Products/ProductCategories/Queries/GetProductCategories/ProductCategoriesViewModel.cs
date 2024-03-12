using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;
namespace MwTech.Application.Products.ProductCategories.Queries.GetProductCategories;

public class ProductCategoriesViewModel
{
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    public ProductCategoryFilter ProductCategoryFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
