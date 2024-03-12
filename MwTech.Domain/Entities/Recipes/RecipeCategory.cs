namespace MwTech.Domain.Entities.Recipes;

public class RecipeCategory
{
    public int Id { get; set; }
    public int OrdinalNumber { get; set; }
    public string Name { get; set; }
    public string? CategoryNumber { get; set; }
    public string Description { get; set; }
    public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
}
