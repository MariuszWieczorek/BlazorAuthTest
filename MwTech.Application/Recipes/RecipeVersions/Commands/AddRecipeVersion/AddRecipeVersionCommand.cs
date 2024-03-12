using MediatR;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Recipes;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Application.Recipes.RecipeVersions.Commands.AddRecipeVersion;

public class AddRecipeVersionCommand : IRequest
{

    [Required(ErrorMessage = "Pole 'Numer Wersji' jest wymagane")]
    [Display(Name = "Numer Wersji")]
    public int VersionNumber { get; set; }

    [Required(ErrorMessage = "Pole 'Numer Wariantu' jest wymagane")]
    [Display(Name = "Numer Wariantu")]
    public int AlternativeNo { get; set; }


    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
    [Display(Name = "Nazwa/Opis")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Domyślna")]
    public bool DefaultVersion { get; set; }

    [Display(Name = "Czy aktywne")]
    public bool IsActive { get; set; }


    [Display(Name = "Produkt")]
    public Recipe Recipe { get; set; }
    public int RecipeId { get; set; }

    [Display(Name = "ilość produktu")]
    public decimal RecipeQty { get; set; } = 1;

    [Display(Name = "waga produktu")]
    public decimal RecipeWeight { get; set; } = 0;

    [Display(Name = "Opis")]
    public string Description { get; set; }


    // akceptacja level 1
    [Display(Name = "akceptacja 1")]
    public bool IsAccepted01 { get; set; }
    [Display(Name = "akceptacja 1 przez")]
    public string Accepted01ByUserId { get; set; }
    [Display(Name = "akceptacja 1 przez")]
    public ApplicationUser Accepted01ByUser { get; set; }
    [Display(Name = "akceptacja 1 czas")]
    public DateTime? Accepted01Date { get; set; }


    // akceptacja level 2
    [Display(Name = "akceptacja 2")]
    public bool IsAccepted02 { get; set; }
    [Display(Name = "akceptacja 2 przez")]
    public string Accepted02ByUserId { get; set; }
    [Display(Name = "akceptacja 2 przez")]
    public ApplicationUser Accepted02ByUser { get; set; }
    [Display(Name = "akceptacja 2 czas")]
    public DateTime? Accepted02Date { get; set; }


    // utworzenie wersji
    public DateTime? CreatedDate { get; set; }
    public string CreatedByUserId { get; set; }
    public ApplicationUser CreatedByUser { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public string ModifiedByUserId { get; set; }
    public ApplicationUser ModifiedByUser { get; set; }

}
