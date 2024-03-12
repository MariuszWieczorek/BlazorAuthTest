using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;

namespace MwTech.Application.GetProductsPropertiesVersions.Queries.GetProductsPropertiesVersions;

public class ProductsPropertiesVersionsViewModel
{
    public List<ProductPropertyVersion> ProductsPropertiesVersions { get; set; }
    public ProductPropertyVersionFilter ProductPropertyVersionFilter { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public PagingInfo PagingInfo { get; set; }
}
