using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Commands.ImportProductsCostFromExcel;

public class ProductCostToImport
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public string Comment { get; set; }
}
