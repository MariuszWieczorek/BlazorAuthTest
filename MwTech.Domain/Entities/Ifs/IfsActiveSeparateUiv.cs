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

public class IfsActiveSeparateUiv
{
    public string WORK_NO { get; set; }
    public string? WORK_CENTER_NO { get; set; }
    public string? MCH_CODE { get; set; }
    public string? ORG_CODE { get; set; }
    public string? DEPARTMENT_NO { get; set; }
    public string? ERR_DESCR { get; set; }
    public string? CONTRACT { get; set; }
    public string? STATE { get; set; }
    public string? WORK_ORDER_SYMPT_CODE { get; set; }

    public DateTime? REG_DATE { get; set; }
    public DateTime? ACTUAL_START { get; set; }
    public DateTime? ACTUAL_FINISH { get; set; }

    [NotMapped]
    [Display(Name = "Test")]
    public string? Description { get; set; }
}

