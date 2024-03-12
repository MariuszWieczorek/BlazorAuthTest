using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Domain.Entities.Recipes;

public class RecipePosition
{
    public int Id { get; set; }
    public int RecipeStageId { get; set; }
    public RecipeStage? RecipeStage { get; set; }

    public int? RecipePositionPackageId { get; set; }
    public RecipePositionsPackage? RecipePositionPackage { get; set; }

    public int PositionNo { get; set; }
    public int PacketNo { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public decimal ProductQty { get; set; }
    public string? Description { get; set; }

    public bool ReturnFromProcessing { get; set; }

    [NotMapped]
    public decimal? MaterialUnitCost { get; set; }

    [NotMapped]
    public decimal? MaterialTotalCost { get; set; }


    [NotMapped]
    public decimal? LabourUnitCost { get; set; }

    [NotMapped]
    public decimal? LabourTotalCost { get; set; }


    [NotMapped]
    public decimal? MarkupUnitCost { get; set; }

    [NotMapped]
    public decimal? MarkupTotalCost { get; set; }

    
    [NotMapped]
    public decimal? UnitCost { get; set; }

    [NotMapped]
    public decimal? TotalCost { get; set; }


    [NotMapped]
    public decimal? PartVolume { get; set; }
    [NotMapped]
    public decimal? PartRubberContent { get; set; }
    


    [NotMapped]
    public decimal? PartQty2 { get; set; }
    [NotMapped]
    public decimal? PartVolume2 { get; set; }
    [NotMapped]
    public decimal? PartRubberContent2 { get; set; }

    [NotMapped]
    public decimal? RunningPartQty2s { get; set; }


    [NotMapped]
    public decimal? PartPhr { get; set; }

    [NotMapped]
    public bool PrevStageSummary { get; set; }


    [NotMapped]
    public decimal? LastStageWeight { get; set; }
    [NotMapped]
    public decimal? LastStagePercent { get; set; }
}
