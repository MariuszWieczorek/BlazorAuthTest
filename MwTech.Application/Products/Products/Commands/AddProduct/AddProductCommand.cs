using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MwTech.Application.Products.Products.Commands.AddProduct;

public class AddProductCommand : IRequest
{
    [Required]
    [Display(Name = "Indeks")]
    public string ProductNumber { get; set; }


    [Display(Name = "KT")]
    public int? TechCardNumber { get; set; }


    [Required]
    [Display(Name = "Nazwa")]
    public string Name { get; set; }


    [Display(Name = "Opis")]
    public string? Description { get; set; }

        
    [Required]
    [Display(Name = "Kategoria")]
    public int ProductCategoryId { get; set; }

    [Display(Name = "Jm")]
    [Required]
    public int UnitId { get; set; }


    [Display(Name = "Zwrot")]
    public bool ReturnedFromProd { get; set; }

    [Display(Name = "Nie licz TKW")]
    public bool NoCalculateTkw { get; set; }


    [Display(Name = "Aktywny")]
    public bool IsActive { get; set; }

    [Display(Name = "Testowy")]
    public bool IsTest { get; set; }


    [Display(Name = "Stary Indeks")]
    public string? OldProductNumber { get; set; }

    public int MwbaseMatid { get; set; }
    public int MwbaseWyrobId { get; set; }


    [Display(Name = "Zawartość kauczuku")]
    public decimal ContentsOfRubber { get; set; }

    [Display(Name = "Gęstość")]
    public decimal Density { get; set; }

    [Display(Name = "Id Wagi")]
    public int ScalesId { get; set; }

    [Display(Name = "Waga [gram]")]
    [NotMapped]
    public decimal ProductWeight { get; set; }


    [Display(Name = "Koszt [Pln]")]
    [NotMapped]
    public decimal ProductCost { get; set; }

    [Display(Name = "Koszt Materiałów [Pln]")]
    [NotMapped]
    public decimal ProductMaterialCost { get; set; }


    [Display(Name = "Koszt Robocizny [Pln]")]
    [NotMapped]
    public decimal ProductLabourCost { get; set; }



    [Display(Name = "Aps01")]
    public string? Aps01 { get; set; }

    [Display(Name = "Aps02")]
    public string? Aps02 { get; set; }

    [Display(Name = "Liczba miejsc dziesiętnych")]
    public int? DecimalPlaces { get; set; }

    [Display(Name = "Tolerancja naważania +/-")]
    public decimal? WeightTolerance { get; set; }

}
