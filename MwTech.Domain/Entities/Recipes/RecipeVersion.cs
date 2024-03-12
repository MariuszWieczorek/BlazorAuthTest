using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Domain.Entities.Recipes;

public class RecipeVersion
{
    public int Id { get; set; }
    public int VersionNumber { get; set; }
    public int AlternativeNo { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool DefaultVersion { get; set; }
    public bool IsActive { get; set; }
    public Recipe? Recipe { get; set; }
    public int RecipeId { get; set; }
    public string? Description { get; set; }
    public decimal RecipeQty { get; set; } = 1;

    // akceptacja level 1
    public bool IsAccepted01 { get; set; }
    public string? Accepted01ByUserId { get; set; }
    public ApplicationUser? Accepted01ByUser { get; set; }
    public DateTime? Accepted01Date { get; set; }


    // akceptacja level 2
    public bool IsAccepted02 { get; set; }
    public string? Accepted02ByUserId { get; set; }
    public ApplicationUser? Accepted02ByUser { get; set; }
    public DateTime? Accepted02Date { get; set; }


    // utworzenie wersji
    public DateTime? CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedByUserId { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }



    [NotMapped]
    public decimal TotalCost { get; set; }
    [NotMapped]
    public decimal MaterialCost { get; set; }
    [NotMapped]
    public decimal LabourCost { get; set; }
    [NotMapped]
    public decimal MarkupCost { get; set; }

    [NotMapped]
    public decimal TotalQty { get; set; }
    [NotMapped]
    public decimal TotalVolume { get; set; }

    public ICollection<RecipeStage> RecipeStages { get; set; } = new HashSet<RecipeStage>();
    public ICollection<RecipeManual> RecipeManuals { get; set; } = new HashSet<RecipeManual>();
}
