using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ProductCostDto
{
    public int? Id { get; set; }
    public int PeriodId { get; set; }
    public string PeriodNumber { get; set; }
    public int ProductId { get; set; }
    public string ProductNumber { get; set; }
    public string ProductName { get; set; }
    public int ProductCategoryId { get; set; }
    public string ProductCategoryName { get; set; }

    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public string? ModifiedByUser { get; set; }
    public DateTime? ModifiedDate { get; set; }


    public bool? IsCalculated { get; set; }
    public bool? IsImported { get; set; }
    public DateTime? CalculatedDate { get; set; }
    public DateTime? ImportedDate { get; set; }

    public string? Description { get; set; }

    public string? CreatedByUser { get; set; }
    public DateTime? CreatedDate { get; set; }
    public decimal? Cost { get; set; }
    public decimal? EstimatedCost { get; set; }
    public decimal? CostInPln { get; set; }
    public decimal? EstimatedCostInPln { get; set; }
}
