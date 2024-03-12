using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Measurements.Measurements.Queries.GetMeasurements;

public class MeasurementFilter
{
    
    [Display(Name = "DataOd")]
   public DateTime? DateTimeFrom { get; set; }

    [Display(Name = "DataDo")]
    public DateTime? DateTimeTo { get; set; }

    [Display(Name = "Kategoria")]
    public int MeasurementCategoryId { get; set; }

    [Display(Name = "Indeks")]
    public string? ProductNumber { get; set; }

}
