using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ProductCostDetail
{
    public int Id { get; set; }
    public int ProductCostId { get; set; }
    
    public Product? Component { get; set; }
    public int ComponentId { get; set; }

    public decimal PartQty { get; set; }
    public decimal Excess { get; set; }

    public decimal TotalComponentCost { get; set; } // całkowity koszt komponentu lub jego cena
    public decimal ComponentLabourCost { get; set; } // sumaryczny koszt robocizny dla komponentu plus z marszruty produktu
    public decimal ComponentMaterialCost { get; set; } // sumaryczny koszt materiałów dla komponentu
    public decimal ComponentMarkupCost { get; set; } // sumaryczny koszt narzutów dla komponentu
    public decimal ProductLabourCost { get; set; } // koszt robocizny tylko z marszruty produktu 


    public bool  IsCalculated { get; set; }
    public bool IsImported { get; set; }
    
    public string? Description { get; set; }
    
}
