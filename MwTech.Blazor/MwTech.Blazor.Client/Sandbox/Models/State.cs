using System.ComponentModel.DataAnnotations;

namespace MwTech.Blazor.Client.Sandbox.Models;

public enum State
{
    [Display(Name = "Nowy")]
    New,

    [Display(Name = "Aktywny")]
    Active,

    [Display(Name = "Zakończony")]
    Closed
}
