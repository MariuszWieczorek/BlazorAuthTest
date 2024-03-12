using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities.Ifs;

public class IfsWorkCenterMaterialRequest
{
    public int Id { get; set; }
    public DateTime ReqDate { get; set; }
    public string OrderNo { get; set; }
    public string PartNo { get; set; }
    public string WorkCenterNo { get; set; }
    public decimal QtyRequired { get; set; }
    public decimal QtyDelivered { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public int ReqState { get; set; }
    public string? SourceLocation { get; set; }

    [NotMapped]
    public List<IfsInventoryPartInStock> IfsInventoryPartsInStock { get; set; } = new List<IfsInventoryPartInStock>();

}

