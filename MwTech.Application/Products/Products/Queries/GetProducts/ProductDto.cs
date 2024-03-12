
namespace MwTech.Application.Products.Products.Queries.GetProducts;
public class ProductDto
{
    public int Id { get; set; }
    public string ProductNumber { get; set; }
    public string OldProductNumber { get; set; }
    public string Name { get; set; }
    public string TechCardNumber { get; set; }
    public string CategoryName { get; set; }
    public string UnitCode { get; set; }

    public decimal ProductWeight { get; set; }
}
