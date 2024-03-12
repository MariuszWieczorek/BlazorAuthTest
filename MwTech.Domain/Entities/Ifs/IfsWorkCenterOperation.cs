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

public class IfsWorkCenterOperation
{
    public string ORDER_NO { get; set; }
    public string PART_NO { get; set; }
    public int OP_ID { get; set; }
    public string? WORK_CENTER_NO { get; set; }
    public string? OPERATION_DESCRIPTION { get; set; }
    public decimal REVISED_QTY_DUE { get; set; }
    public decimal QTY_COMPLETE { get; set; }
    public DateTime OP_START_DATE { get; set; }
    public DateTime OP_FINISH_DATE { get; set; }

    // public decimal REMAINING_QTY { get; set; }
    public string? Shift { get; set; }

    [NotMapped]
    public decimal ProductWeight { get; set; }

    [NotMapped]
    [Display(Name = "Parametry")]
    public List<ProductProperty> Params { get; set; } = new List<ProductProperty>();

    [NotMapped]
    [Display(Name = "Zastosowanie Komponentu")]
    public List<ComponentUsage> ComponentUsages { get; set; } = new List<ComponentUsage>();
}

