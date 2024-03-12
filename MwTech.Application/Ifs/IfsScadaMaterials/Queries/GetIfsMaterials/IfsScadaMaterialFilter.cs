using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsScadaMaterials.Queries.GetIfsMaterials;

public class IfsScadaMaterialFilter
{
    [Display(Name = "Id Operacji")]
    public int OpId { get; set; }

    [Display(Name = "Indeks Produktu")]
    public string PartNo { get; set; }

    [Display(Name = "Zamówienie")]
    public string OrderNo { get; set; }

    [Display(Name = "Gniazdo")]
    public string WorkCenterNo { get; set; }


}
