namespace MwTech.Application.Products.Routes.Commands.ImportRoutesFromExcel;

// klasa pomocnicza do importu struktury produktu
public class RouteToImport
{
    public int RouteAltNo { get; set; }
    public int RouteVersionNo { get; set; }
    public string RouteAltName { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public int ProductId { get; set; }
    public int No { get; set; }
    public int OperationId { get; set; }
    public int WorkCenterId { get; set; }
    public int ResourceId { get; set; }
    public Decimal ResourceQty { get; set; }
    public int? ChangeOverResourceId { get; set; }
    public Decimal ChangeOverNumberOfEmployee { get; set; }
    public decimal OperationLabourConsumption { get; set; }
    public decimal OperationMachineConsumption { get; set; }
    public decimal ChangeOverLabourConsumption { get; set; }
    public decimal ChangeOverMachineConsumption { get; set; }
    public decimal MoveTime { get; set; }
    public decimal OverLap { get; set; }
    public int? ProductCategoryId { get; set; }
}
