using MwTech.Domain.Entities;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.CreateProduct;

public class RecipeProduct
{
    public string ProductNumber { get; set; }
    public ProductCategory ProductCategory  { get; set; }
    public string Name { get; set; }
    public int ProductCategoryId { get; set; }
    public int UnitId { get; set; }
    public bool ProductInDb { get; set; }
    public int RecipeId { get; set; }
    public int? StageId { get; set; }
    public int? PackageId { get; set; }
    public int VersionNumber { get; set; }
    public int AlternativeNo { get; set; }
    public decimal ProductQty { get; set; }
    public int CalculateWeightOrder { get; set; }

}
