using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Commands.ImportProductsFromExcel;

public class ProductToImport
{
    public string ProductNumber { get; set; }
    public string? OldProductNumber { get; set; }
    public string? Idx01 { get; set; }
    public string? Idx02 { get; set; }
    public string Name { get; set; }
    public int ProductCategoryId { get; set; }
    public int UnitId { get; set; }
    public bool ProductInDb { get; set; }
}
