using MediatR;
using MwTech.Application.Products.Common;

namespace MwTech.Application.Measurements.Measurements.Queries.GetAddMeasurementViewModel;

public class GetAddMeasurementViewModelQuery : IRequest<AddMeasurementViewModel>
{
    public int ProductId { get; set; }
    public ProductFilter ProductFilter { get; set; }

}
