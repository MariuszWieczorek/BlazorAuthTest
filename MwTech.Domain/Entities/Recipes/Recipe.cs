using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Recipes;

public class Recipe
{
    public int Id { get; set; }
    public string RecipeNumber { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int RecipeCategoryId { get; set; }
    public RecipeCategory? RecipeCategory { get; set; }
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
    public decimal MaterialUnitCostWithoutProcessReturn { get; set; }

    [NotMapped]
    public decimal TotalQty { get; set; }
    [NotMapped]
    public decimal TotalVolume { get; set; }

    [NotMapped]
    public bool IsAccepted01 { get; set; }
    
    [NotMapped]
    public bool IsAccepted02 { get; set; }

    [NotMapped]
    public string VersionName { get; set; }

    [NotMapped]
    public bool HasUserManual { get; set; }

    [NotMapped]
    public int VersionId { get; set; }

    public string? ScrapNumber { get; set; }

    [NotMapped]
    public string? ProductNumber { get; set; }



    public ICollection<RecipeVersion> RecipeVersions { get; set; } = new HashSet<RecipeVersion>();
}
