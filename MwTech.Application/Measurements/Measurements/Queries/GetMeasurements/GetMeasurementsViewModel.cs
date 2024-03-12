using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Measurements;

namespace MwTech.Application.Measurements.Measurements.Queries.GetMeasurements;

public class GetMeasurementsViewModel
{
    public IEnumerable<MeasurementPosition> Measurements { get; set; }
    public MeasurementFilter MeasurementFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
