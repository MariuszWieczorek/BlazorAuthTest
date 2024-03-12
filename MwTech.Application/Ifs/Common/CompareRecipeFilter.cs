using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Ifs.Common;

public class CompareRecipeFilter
{
    [Display(Name = "Indeks")]
    public string ProductNumber { get; set; }

    [Display(Name = "Pokaż testowe")]
    public bool ShowTestProducts { get; set; }
}
