using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ScadaReport
{
    public Int64 SEQ_ID { get; set; }
    public Int64 OP_ID { get; set; }
    public DateTime TIMESTAMP { get; set; }
    public string TYPE { get; set; } // REPORT | ISSUE
    public string? PART_NO { get; set; }
    public string? REPORTED_BY { get; set; }
    public decimal QTY_REPORTED { get; set; }
    public decimal QTY_ISSUED { get; set; }
    public decimal TIME_CONSUMED { get; set; }
    public string? LOT_BATCH_NO { get; set; }
    public string? HANDLING_UNIT_ID { get; set; }
    public string? STATUS { get; set; }
    public string? WORK_CENTER_NO { get; set; }
    public string? RESOURCE_ID { get; set; }
    public string? DESCRIPTION { get; set; }
    public DateTime? TIME_START { get; set; }
    public DateTime? TIME_STOP { get; set; }
  
}
