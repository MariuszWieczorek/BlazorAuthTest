using MwTech.Application.Products.Products.Queries.GetProducts;
using MwTech.Application.Products.Products.Queries.GetProductsDataTable;
using MwTech.Domain.Entities;


namespace MwTech.Application.Products.Products.Extensions;
public static class ProductExtensions
{
    

    public static ProductDto ToProductDto(this Product product)
    {
        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            ProductNumber = product.ProductNumber,
            Name = product.Name,
            UnitCode = product.Unit.UnitCode,
            CategoryName = product.ProductCategory.Name
        };
    }

    public static ProductDataTableDto ToProductDataTableDto(this Product product)
    {
        if (product == null)
            return null;

        return new ProductDataTableDto
        {
            Id = product.Id,
            ProductNumber = product.ProductNumber,
            Name = product.Name,
            UnitCode = product.Unit.UnitCode,
            CategoryName = product.ProductCategory.Name
        };
    }
}
