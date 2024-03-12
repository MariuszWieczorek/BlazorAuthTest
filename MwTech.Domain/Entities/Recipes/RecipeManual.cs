using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Domain.Entities.Recipes;

public class RecipeManual
{
    public int Id { get; set; }
    public int RecipeVersionId { get; set; }
    public RecipeVersion? RecipeVersion { get; set; }
    public int RecipeStageId { get; set; }
    public RecipeStage? RecipeStage { get; set; }
    public int PositionNo { get; set; }
    public string? Description { get; set; }
    public decimal? Duration { get; set; }
    public string? TextValue { get; set; }
}
