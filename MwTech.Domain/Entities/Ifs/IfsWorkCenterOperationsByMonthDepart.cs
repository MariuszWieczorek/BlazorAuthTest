using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;


public class IfsWorkCenterOperationsByMonthDepart
{
    public string? OpStartMonthYear { get; set; }
    public string? DepartmentNo { get; set; }
    public string? WorkCenterNo { get; set; }
    public string? UnitMeas { get; set; }

    public decimal? QtyComplete { get; set; }
    public decimal? RevisedQtyDue { get; set; }
    public decimal? QtyPercentComplete { get; set; }

    public decimal? PlannedProductionTime { get; set; }
    public decimal? RealProductionTime { get; set; }
    public decimal? TimePercentComplete { get; set; }


    public decimal? TotalWeightRevised { get; set; }
    public decimal? TotalWeightCompleted { get; set; }
    public decimal? WeightPercentComplete { get; set; }

}

