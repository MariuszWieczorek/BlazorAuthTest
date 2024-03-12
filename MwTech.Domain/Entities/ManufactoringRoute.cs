using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ManufactoringRoute
{
    public int Id { get; set; }
    public RouteVersion? RouteVersion { get; set; }
    public int RouteVersionId { get; set; }
    public int OrdinalNumber { get; set; }
    public Operation? Operation { get; set; }
    public int OperationId { get; set; }
    public int WorkCenterId { get; set; }
    public Resource? WorkCenter { get; set; }
    public int ResourceId { get; set; }
    public Decimal ResourceQty { get; set; }
    public Resource? Resource { get; set; }
    public Decimal OperationLabourConsumption { get; set; }
    public Decimal OperationMachineConsumption { get; set; }
    public int? ChangeOverResourceId { get; set; }
    public Resource? ChangeOverResource { get; set; }
    public decimal ChangeOverNumberOfEmployee { get; set; }
    public decimal ChangeOverLabourConsumption { get; set; }
    public decimal ChangeOverMachineConsumption { get; set; }
    public decimal Overlap { get; set; }
    public decimal MoveTime { get; set; }
    public string? Description { get; set; }
    public int? ProductCategoryId { get; set; }
    public ProductCategory? ProductCategory { get; set; }

    public int? RoutingToolId { get; set; }
    public RoutingTool? RoutingTool { get; set; }
    public int? ToolQuantity { get; set; }

    [NotMapped]
    public decimal CostWithoutMarkup { get; set; }
    [NotMapped]
    public decimal TotalCost { get; set; }

}