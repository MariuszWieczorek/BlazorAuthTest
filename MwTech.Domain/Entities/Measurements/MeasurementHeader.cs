namespace MwTech.Domain.Entities.Measurements;

public class MeasurementHeader
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Shift { get; set; }

    public DateTime? CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedByUserId { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }

    public ICollection<MeasurementPosition> MeasurementPositions { get; set; } = new HashSet<MeasurementPosition>();
}
