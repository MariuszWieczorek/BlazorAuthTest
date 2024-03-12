using MwTech.Application.Products.Boms.Commands.AddBom;
using MwTech.Application.Products.Products.Queries.GetProductsForPopup;

namespace MwTech.Application.Products.Boms.Queries.GetAddBomViewModel;

public class AddBomViewModel
{
    public AddBomCommand AddBomCommand { get; set; }
    public ProductsForPopupViewModel GetProductsForPopupViewModel { get; set; }
}
