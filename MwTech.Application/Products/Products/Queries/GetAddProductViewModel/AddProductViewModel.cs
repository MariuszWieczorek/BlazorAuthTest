using MwTech.Application.Products.Products.Commands.AddProduct;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.Products.Queries.GetAddProductViewModel;

public class AddProductViewModel
{
    public AddProductCommand AddProductCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public List<Unit> Units { get; set; }

}
