using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.MachineCategories.Commands.EditMachineCategory;
public class EditMachineCategoryCommand : IRequest
{
    public int Id { get; set; }

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
