using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Queries.GetMaterialsRequirements;

public class MaterialRequirementFilter
{

    [Display(Name = "Indeks Produktu")]
    public string PartNo { get; set; }

    [Display(Name = "Zlecenie")]
    public string OrderNo { get; set; }

    [Display(Name = "Gniazdo")]
    public string WorkCenterNo { get; set; }

}
