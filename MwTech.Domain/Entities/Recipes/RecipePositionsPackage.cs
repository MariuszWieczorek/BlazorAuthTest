using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Domain.Entities.Recipes;

public class RecipePositionsPackage
{
    public int Id { get; set; }
    public int PackageNumber { get; set; }
    public string? ProductNumber { get; set; }
    public string? ProductName { get; set; }
    public int RecipeStageId { get; set; }
    public bool BagIsIncluded { get; set; }
    public RecipeStage? RecipeStage { get; set; }
    public string? Description { get; set; }

    public int? WorkCenterId { get; set; }
    public Resource? WorkCenter { get; set; }

    public int? LabourClassId { get; set; }
    public Resource? LabourClass { get; set; }

    public decimal? CrewSize { get; set; }
    public int TimeInSeconds { get; set; }

    public ICollection<RecipePosition> RecipePositions { get; set; } = new HashSet<RecipePosition>();

    [NotMapped]
    public decimal? TotalQty { get; set; }
    
    [NotMapped]
    public decimal? LabourTotalCost { get; set; }

    [NotMapped]
    public decimal? MarkupTotalCost { get; set; }

    [NotMapped]
    public decimal? UnitPerHour { get; set; }
}
