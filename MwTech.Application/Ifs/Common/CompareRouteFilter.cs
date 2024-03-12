using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Ifs.Common;

public class CompareRouteFilter
{
    [Display(Name = "Indeks")]
    public string ProductNumber { get; set; }

    [Display(Name = "Idx")]
    public int IdxNo { get; set; }
}
