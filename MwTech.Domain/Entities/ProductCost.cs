using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ProductCost
{
    public int Id { get; set; }
    public AccountingPeriod? AccountingPeriod { get; set; }
    public int AccountingPeriodId { get; set; }
    public Product? Product { get; set; }
    public int ProductId { get; set; }
    public decimal Cost { get; set; } // całkowity koszt komponentu lub jego cena
    public decimal LabourCost { get; set; } // sumaryczny koszt robocizny dla komponentu plus z marszruty produktu
    public decimal MaterialCost { get; set; } // sumaryczny koszt materiałów dla komponentu
    public decimal MarkupCost { get; set; } // sumaryczny koszt narzutów dla komponentu
    public decimal ProductLabourCost { get; set; } // koszt robocizny tylko z marszruty produktu 


    // dla symulacji szacunkowe koszty

    public decimal EstimatedCost { get; set; } // całkowity koszt komponentu lub jego cena
    public decimal EstimatedLabourCost { get; set; } // sumaryczny koszt robocizny dla komponentu plus z marszruty produktu
    public decimal EstimatedMaterialCost { get; set; } // sumaryczny koszt materiałów dla komponentu
    public decimal EstimatedMarkupCost { get; set; } // sumaryczny koszt narzutów dla komponentu
    public decimal EstimatedProductLabourCost { get; set; } // koszt robocizny tylko z marszruty produktu 

    public bool  IsCalculated { get; set; }
    public bool IsImported { get; set; }
    public DateTime? CalculatedDate { get; set; }
    public DateTime? ImportedDate { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }
    public string? CreatedByUserId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? Description { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    public string? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public int? CurrencyId { get; set; }
    public Currency? Currency { get; set; }

    [NotMapped]
    public decimal CostInPln { get; set; }
}
