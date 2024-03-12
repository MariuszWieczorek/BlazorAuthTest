namespace MwTech.Application.Products.RouteVersions.Commands.ImportDefaultRouteVersion;

// klasa pomocnicza do importu struktury produktu
public class RouteVersionToSetAsDefault
{
    public int ProductId { get; set; }
    public int RouteAltNo { get; set; }
    public int RouteVersionNo { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }

}
