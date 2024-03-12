namespace MwTech.Domain.Entities.Recipes;

public class RecipeSummary
{
    public bool IsAccepted01 { get; set; }
    public bool IsAccepted02 { get; set; }
    public string VersionName { get; set; }
    public int VersionId { get; set; }
    public decimal TotalCost { get; set; }
    public decimal LabourCost { get; set; }
    public decimal MaterialCost { get; set; }
    public decimal MarkupCost { get; set; }
    public decimal TotalQty { get; set; }
    public decimal TotalVolume { get; set; }

    public string ScrapNumber { get; set; }
    public string ProductNumber { get; set; }

    // dodane 2023.11.20
    public decimal RubberContent { get; set; }
    public decimal Density { get; set; }

    public decimal MaterialUnitCostWithoutProcessReturn { get; set; }

}
