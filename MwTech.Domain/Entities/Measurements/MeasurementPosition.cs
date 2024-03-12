using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Domain.Entities.Measurements;

public class MeasurementPosition
{
    public int Id { get; set; }
    public int MeasurementHeaderId { get; set; }
    public MeasurementHeader MeasurementHeader { get; set; }
    public decimal Value { get; set; }


    [NotMapped]
    public decimal? TargetValue { get; set; }
    [NotMapped]
    public decimal? MinValue { get; set; }
    [NotMapped]
    public decimal? MaxValue { get; set; }

}
