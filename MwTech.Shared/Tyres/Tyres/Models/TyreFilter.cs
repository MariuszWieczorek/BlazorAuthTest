using System.ComponentModel.DataAnnotations;

namespace MwTech.Shared.Tyres.Tyres.Models;

public class TyreFilter
{
    [Display(Name = "Nazwa")]
    public string TyreName { get; set; }

    [Display(Name = "Indeks")]
    public string TyreNumber { get; set; }

}
