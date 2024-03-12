using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Domain.Entities.Recipes;

public class RecipeStage
{


    public int Id { get; set; }
    public RecipeVersion? RecipeVersion { get; set; }
    public int RecipeVersionId { get; set; }
    public int StageNo { get; set; }
    public string? StageName { get; set; }
    public string? ProductNumber { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }

    public ICollection<RecipePosition> RecipePositions { get; set; } = new HashSet<RecipePosition>();
    public ICollection<RecipePositionsPackage> RecipePositionsPackages { get; set; } = new HashSet<RecipePositionsPackage>();
    public ICollection<RecipeManual> RecipeManuals { get; set; } = new HashSet<RecipeManual>();

    public decimal MixerVolume { get; set; }
    public decimal? PrevStageQty { get; set; }

    

    public decimal? DivideQtyBy { get; set; }
    public decimal? MultiplyQtyBy { get; set; }

    public int? WorkCenterId { get; set; }
    public Resource? WorkCenter { get; set; }


    public int? LabourClassId { get; set; }
    public Resource? LabourClass { get; set; }

    public decimal? CrewSize { get; set; }

    public decimal? LabourRunFactor { get; set; }


    public int StageTimeInSeconds { get; set; }

    [NotMapped]
    public decimal? TotalQty { get; set; }

    [NotMapped]
    public decimal? TotalRubberContent { get; set; }
    [NotMapped]
    public decimal? TotalVolume { get; set; }
    [NotMapped]
    public decimal? TotalPhr { get; set; }

    [NotMapped]
    public decimal? TotalQty2 { get; set; }

    [NotMapped]
    public decimal? TotalVolume2 { get; set; }




    [NotMapped]
    public decimal? TotalRubberContent2 { get; set; }


    [NotMapped]
    public decimal? StageDensity { get; set; }

    [NotMapped]
    public decimal? StagePercentRubberContent { get; set; }

    
    /* Koszty Materiałów */
    [NotMapped]
    public decimal? MaterialTotalCost { get; set; }

    [NotMapped]
    public decimal? MaterialUnitCost { get; set; }

    
    /* Koszty Robocizny - dla całego wsadu */
    [NotMapped]
    public decimal? StageLabourMixingCost { get; set; }

    [NotMapped]
    public decimal? StageLabourPackingCost { get; set; }

    [NotMapped]
    public decimal? StageLabourCost { get; set; }
    
        [NotMapped]
    public decimal? PositionsLabourCost { get; set; }

    [NotMapped]
    public decimal? TotalLabourCost { get; set; }


    /* Koszty Robocizny - jednostkowe */

    [NotMapped]
    public decimal? StageUnitLabourCost { get; set; }

    [NotMapped]
    public decimal? PositionsUnitLabourCost  { get; set; }

    [NotMapped]
    public decimal? UnitLabourCost { get; set; }



    /* Narzut */
    /* Koszty Narzutu - dla całego wsadu */
    [NotMapped]
    public decimal? StageMarkupMixingCost { get; set; }

    [NotMapped]
    public decimal? StageMarkupPackingCost { get; set; }

    [NotMapped]
    public decimal? StageMarkupCost  { get; set; }
    

    [NotMapped]
    public decimal? PositionsMarkupCost { get; set; }

    [NotMapped]
    public decimal? TotalMarkupCost { get; set; }


    /* Koszty Narzutu - jednostkowe */

    [NotMapped]
    public decimal? StageUnitMarkupCost { get; set; }

    [NotMapped]
    public decimal? PositionsUnitMarkupCost { get; set; }

    [NotMapped]
    public decimal? UnitMarkupCost { get; set; }


    /* Koszty Razem */
    [NotMapped]
    public decimal? TotalCost { get; set; }
    [NotMapped]
    public decimal? UnitCost { get; set; }




    [NotMapped]
    public decimal? TotalQtyWithoutProcessReturn { get; set; }


    [NotMapped]
    public decimal? MaterialTotalCostWithoutProcessReturn { get; set; }

    [NotMapped]
    public decimal? MaterialUnitCostWithoutProcessReturn { get; set; }


    [NotMapped]
    public decimal? TotalWeightTolerance { get; set; }


    // do wyliczania procentów
    [NotMapped]
    public decimal? TotalStagePercent { get; set; }


    [NotMapped]
    public decimal? Factor { get; set; }

    [NotMapped]
    public decimal? UnitPerHour { get; set; }

}
