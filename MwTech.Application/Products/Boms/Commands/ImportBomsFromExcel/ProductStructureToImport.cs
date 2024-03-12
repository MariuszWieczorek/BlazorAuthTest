using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Boms.Commands.ImportBomsFromExcel;

public class ProductStructureToImport
{
    public int AltNo { get; set; }
    public string AltName { get; set; }
    public int VersionNo { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public int SetId { get; set; }
    public int No { get; set; }
    public int PartId { get; set; }
    public decimal Qty { get; set; }
    public decimal Excess { get; set; }
    public bool OnProductionOrder { get; set; }
    public int Layer { get; set; }
}
