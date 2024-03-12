using MwTech.Application.Products.Boms.Commands.EditBom;
using MwTech.Application.Products.Products.Queries.GetProductsForPopup;

namespace MwTech.Application.Products.Boms.Queries.GetEditBomViewModel;

public class EditBomViewModel
{
    public EditBomCommand EditBomCommand { get; set; }
    public ProductsForPopupViewModel GetProductsForPopupViewModel { get; set; }
}
