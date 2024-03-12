using MwTech.Application.Measurements.Measurements.Commands.AddMeasurement;
using MwTech.Application.Products.Products.Queries.GetProductsForPopup;

namespace MwTech.Application.Measurements.Measurements.Queries.GetAddMeasurementViewModel;

public class AddMeasurementViewModel
{
    public AddMeasurementCommand AddMeasurementCommand { get; set; }
    public ProductsForPopupViewModel GetProductsForPopupViewModel { get; set; }
}
