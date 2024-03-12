namespace MwTech.Application.Products.ProductProperties.Commands.ImportProductPropertiesFromExcel;

// klasa pomocnicza do importu struktury produktu
public class ProductPropertyToImport
{
    public int ProductId { get; set; }
    public int ProductCategoryId { get; set; }
    public int AltNo { get; set; }
    public int VersionNo { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public string VersionName { get; set; }
    public int PropertyId { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? Value { get; set; }
    public decimal? MaxValue { get; set; }
    public string Text { get; set; }
}
