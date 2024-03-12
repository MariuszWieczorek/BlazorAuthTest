using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.Recipes.Recipes.Commands.EditRecipe;

public class EditRecipeCommand : IRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Pole 'Symbol' jest wymagane")]
    [Display(Name = "Symbol")]
    public string RecipeNumber { get; set; }

    
    [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane")]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }


    [Display(Name = "Opis")]
    public string? Description { get; set; }


    [Required(ErrorMessage = "Pole 'Kategoria Receptury' jest wymagane")]
    [Display(Name = "Kategoria Receptury")]
    public int RecipeCategoryId { get; set; }

    [Display(Name = "Aktywny")]
    public bool IsActive { get; set; }

    [Display(Name = "Testowy")]
    public bool IsTest { get; set; }

}
