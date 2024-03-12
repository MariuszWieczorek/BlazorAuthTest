using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;

public class IfsInventoryPartInStock
{
    public string PartNo { get; set; }
    public string LocationNo { get; set; }
    public string LocationName { get; set; }
    public decimal QtyOnHand { get; set; }
    public decimal QtyReserved { get; set; }
    public decimal QtyInTransit { get; set; }
    public decimal QtyAvailable { get; set; }
    public string HandlingUnitId { get; set; }
    public DateTime ReceiptDate { get; set; }
}

