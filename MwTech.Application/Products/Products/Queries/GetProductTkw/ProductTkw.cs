using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetProductTkw;

public class ProductTkw
{
    public int Level { get; set; }
    public int ProductId { get; set; }
    public int ProductVersionId { get; set; }
    public String ProductNumber { get; set; }

    public decimal TotalCost { get; set; }
    public decimal LabourCost { get; set; }
    public decimal MaterialCost { get; set; }

    public bool AllowRecalculate { get; set; }
}
