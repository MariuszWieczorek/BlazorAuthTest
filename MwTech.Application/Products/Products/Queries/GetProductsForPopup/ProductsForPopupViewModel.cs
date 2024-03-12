using MwTech.Application.Common.Models;
using MwTech.Application.Products.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.Products.Queries.GetProductsForPopup;

public class ProductsForPopupViewModel
{
    public ProductFilter ProductFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    public IEnumerable<Product> Products { get; set; }
    public AccountingPeriod CurrentPeriod { get; set; }

    public int ProductsCount { get; set; }

}
