using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.MachineCategories.Commands.AddMachineCategory;

public class AddMachineCategoryCommand : IRequest
{
    [Display(Name = "Lp")]
    public int OrdinalNumber { get; set; }

    [Required]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Display(Name = "Symbol")]
    public string? MachineCategoryNumber { get; set; }

    [Display(Name = "Opis")]
    public string Description { get; set; }

    [Display(Name = "Kategoria Produktu")]
    public int? ProductCategoryId { get; set; }
}
