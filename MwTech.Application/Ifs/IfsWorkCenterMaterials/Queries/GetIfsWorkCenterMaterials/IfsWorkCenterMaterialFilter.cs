using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Queries.GetIfsWorkCenterMaterials;

public class IfsWorkCenterMaterialFilter
{

    [Display(Name = "Indeks Produktu")]
    public string PartNo { get; set; }

    [Display(Name = "Zlecenie")]
    public string OrderNo { get; set; }

    [Display(Name = "Gniazdo")]
    public string WorkCenterNo { get; set; }

    [Display(Name = "Zmiana")]
    public string Shift { get; set; }

}
