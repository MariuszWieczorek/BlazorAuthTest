using MediatR;
using MwTech.Application.Common.Models;

namespace MwTech.Application.Measurements.Measurements.Queries.GetMeasurements;

public class GetMeasurementsQuery : IRequest<GetMeasurementsViewModel>
{
    public MeasurementFilter MeasurementFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public int Id { get; set; }
}
