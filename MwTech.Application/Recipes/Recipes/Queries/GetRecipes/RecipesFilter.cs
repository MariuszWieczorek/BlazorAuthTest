using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Recipes.Recipes.Queries.GetRecipes;

public class RecipeFilter
{
    [Display(Name = "Nazwa")]
    public string? Name { get; set; }

    [Display(Name = "Symbol")]
    public string? RecipeNumber { get; set; }

    [Display(Name = "Kategoria Receptury")]
    public int RecipeCategoryId { get; set; }

}
