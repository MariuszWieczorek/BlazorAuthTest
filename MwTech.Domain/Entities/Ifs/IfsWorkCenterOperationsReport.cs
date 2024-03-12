using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;


public class IfsWorkCenterOperationsReport
{
    public string? DepartmentNo { get; set; }
    public string? WorkCenterNo { get; set; }
    public string? ProductionLine { get; set; }
    public string? OrderNo { get; set; }
    public string? SequenceNo { get; set; }
    public int? OperationId { get; set; }
    public string? ProductNumber { get; set; }
    public string? OpStartDay { get; set; }
    public string? OpStartMonthYear { get; set; }
    public DateTime? OpStartDate { get; set; }
    public DateTime? OpFinishDate { get; set; }
    public DateTime? RealStartedDate { get; set; }
    public decimal? RevisedQtyDue { get; set; }
    public decimal? QtyComplete { get; set; }
    public decimal? PlannedProductionTime { get; set; }
    public decimal? RealProductionTime { get; set; }
    public string? UnitMeas { get; set; }
    public string? RowState { get; set; }
    public string? RouteAltDescr { get; set; }
    public string? StructureAltDescr { get; set; }
    public int NumberOfEmployee { get; set; }
    public string? Priority { get; set; }
    public string? Shift { get; set; }
    public decimal? WeightNet { get; set; }
    public decimal? TotalWeightRevised { get; set; }
    public decimal? TotalWeightCompleted { get; set; }

    [NotMapped]
    public decimal ProductWeight { get; set; }

    [NotMapped]
    public decimal PercentQty { get; set; }

    [NotMapped]
    public decimal PercentTime { get; set; }

    [NotMapped]
    public decimal PercentWeight { get; set; }

}

