using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Application.Settings.Commands.EditSetting;

public class EditSettingCommand : IRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Pole 'Symbol' jest wymagane")]
    [Display(Name = "Symbol")]
    public string SettingNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Pole 'Lp' jest wymagane")]
    [Display(Name = "Lp")]
    public int OrdinalNumber { get; set; }

    [Display(Name = "Opis")]
    public string? Description { get; set; }

    [Display(Name = "Kategoria Ustawień")]
    public int SettingCategoryId { get; set; }

    [Display(Name = "Kategoria Maszyny")]
    public int MachineCategoryId { get; set; }

    [Display(Name = "Wartość Tekstowa")]
    public string? Text { get; set; }

    [Display(Name = "Wartość Min")]
    public decimal? MinValue { get; set; }

    [Display(Name = "Wartość")]
    public decimal? Value { get; set; }

    [Display(Name = "Wartość Max")]
    public decimal? MaxValue { get; set; }

    [Display(Name = "Edycja")]
    public bool IsEditable { get; set; }

    [Display(Name = "Aktywne")]
    public bool IsActive { get; set; }

    [Display(Name = "Numeryczne")]
    public bool IsNumeric { get; set; }

    [Display(Name = "Zawsze na wydruku")]
    public bool AlwaysOnPrintout { get; set; }

    [Display(Name = "Ukryj na Wydruku")]
    public bool HideOnPrintout { get; set; }

    [Display(Name = "jm")]
    public int UnitId { get; set; }

}
