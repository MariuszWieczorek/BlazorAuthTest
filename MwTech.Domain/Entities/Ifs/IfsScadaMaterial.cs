using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;


public class IfsScadaMaterial
{
    public int OP_ID { get; set; }
    public string PART_NO { get; set; }
    public string? OBJSTATE { get; set; }
    public string? ORDERRNO { get; set; }
    public int OPERATION_NO { get; set; }
    public string? OPERATION_DESCRIPTION { get; set; }
    public string? WORK_CENTER_NO { get; set; }
    public string? PREFERRED_RESOURCE_ID { get; set; }

    public string COMPONENT_PART_NO { get; set; }

    public decimal QTY_REQUIRED { get; set; }
    public decimal QTY_ISSUED { get; set; }
    public decimal QTY_LEFT { get; set; }

    //public decimal QTY_PER_ASSEMBLY { get; set; }

    public DateTime OP_START_DATE { get; set; }
    public DateTime OP_FINISH_DATE { get; set; }

    [NotMapped]
    [Display(Name = "Ilość zaraportowana")]
    public decimal QTY_SCADA_REPORTED { get; set; }

    [NotMapped]
    [Display(Name = "Ilość zaraportowana")]
    public decimal QTY_SCADA_ISSUED { get; set; }

}
