namespace MwTech.Application.Products.Common;

public class SimpleProductWithWeight
{
    public int ProductId { get; set; }
    public string ProductNumber { get; set; }
    public string Name { get; set; }
    public decimal? WeightInKg { get; set; }
    public decimal? MinWeightInKg { get; set; }
    public decimal? MaxWeightInKg { get; set; }
    public bool Success { get; set; }
}
