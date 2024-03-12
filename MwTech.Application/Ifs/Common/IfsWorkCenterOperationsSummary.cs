using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.Common;

public class IfsWorkCenterOperationsSummary
{
    public decimal TotalPlannedWeight { get; set; }
    public decimal TotalRealWeight { get; set; }
    public decimal TotalPlannedQty { get; set; }
    public decimal TotalRealQty { get; set; }
    public decimal TotalPlannedTime { get; set; }
    public decimal TotalRealTime { get; set; }
    public decimal WeightPercentComplete { get; set; }
    public decimal TimePercentComplete { get; set; }
    public decimal QtyPercentComplete { get; set; }
}
