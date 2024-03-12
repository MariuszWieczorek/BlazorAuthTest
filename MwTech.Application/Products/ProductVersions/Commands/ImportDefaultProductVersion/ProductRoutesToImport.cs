namespace MwTech.Application.Products.ProductVersions.Commands.ImportDefaultProductVersion;

// klasa pomocnicza do importu struktury produktu
public class ProductVersionToSetAsDefault
{
    public int ProductId { get; set; }
    public int ProductAltNo { get; set; }
    public int ProductVersionNo { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }

}
