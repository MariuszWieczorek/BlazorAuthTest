using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Ifs.Common;

public class CompareStructureFilter
{
    [Display(Name = "Indeks")]
    public string ProductNumber { get; set; }
}
