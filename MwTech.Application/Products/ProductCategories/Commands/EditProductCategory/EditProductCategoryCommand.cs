using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Products.ProductCategories.Commands.EditProductCategory;
public class EditProductCategoryCommand : IRequest
{
    public int Id { get; set; }

    [Display(Name = "Lp")]
    public int OrdinalNumber { get; set; }

    [Display(Name = "źródło marszrut")]
    public int RouteSource { get; set; }

    [Required]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }

    [Display(Name = "Symbol")]
    public string? CategoryNumber { get; set; }

    [Display(Name = "Numer Karty Technologicznej")]
    public string? TechCardNumber { get; set; }

    [Display(Name = "Opis")]
    public string Description { get; set; }

    [Display(Name = "Wliczaj Nadmiar Do TKW")]
    public bool TkwCountExcess { get; set; }

    [Display(Name = "Nie licz TKW")]
    public bool NoCalculateTkw { get; set; }

}
