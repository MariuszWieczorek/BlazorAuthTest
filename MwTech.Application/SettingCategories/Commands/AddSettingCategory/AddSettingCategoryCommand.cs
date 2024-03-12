using MediatR;
using MwTech.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.SettingCategories.Commands.AddSettingCategory;

public class AddSettingCategoryCommand : IRequest
{

    [Required(ErrorMessage = "Pole 'Symbol' jest wymagane")]
    [Display(Name = "Symbol")]
    public string SettingCategoryNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Pole 'Lp' jest wymagane")]
    [Display(Name = "Lp")]
    public int OrdinalNumber { get; set; }

    [Display(Name = "Opis")]
    public string? Description { get; set; }

    [Display(Name = "Kolor")]
    public string? Color { get; set; }

    [Required(ErrorMessage = "Pole 'Kategoria Maszyny' jest wymagane")]
    [Display(Name = "Kategoria Maszyny")]
    public int MachineCategoryId { get; set; }

    [Required(ErrorMessage = "Pole 'Kategoria Produktu' jest wymagane")]
    [Display(Name = "Kategoria Produktu")]
    public int ProductCategoryId { get; set; }

}
