using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;

// OP_ID, OBJSTATE, ORDERRNO, PART_NO, OPERATION_NO, OPERATION_DESCRIPTION, WORK_CENTER_NO,
// PREFERRED_RESOURCE_ID, OP_START_DATE, OP_FINISH_DATE, QTY_LEFT, OBJID

public class IfsWorkCenterMaterial
{
    public string OrderNo { get; set; }
    public string SetNo { get; set; }
    public string? WorkCenterNo { get; set; }
    public string? OperationDescription { get; set; }
    public decimal RevisedQtyDue { get; set; }
    public decimal QtyComplete { get; set; }
    public DateTime OpStartDate { get; set; }
    public DateTime OpFinishDate { get; set; }
    public string PartNo { get; set; }
    public decimal? QtyRequired { get; set; }
    public decimal? QtyAvailable { get; set; }
    public decimal? QtyOnInboundLocations { get; set; }
    public string? PrintUnit { get; set; }
    public string? SourceLocation { get; set; }
    public string? ProposedLocation { get; set; }
    public string? OperationState { get; set; }
    public string? OperStatusCode { get; set; }
    public string? MaterialState { get; set; }
    public string? Shift { get; set; }

    [NotMapped]
    public decimal ReportedQty { get; set; }
    [NotMapped]
    public int ReqId { get; set; }

    [NotMapped]
    public int ReqState { get; set; }


}

