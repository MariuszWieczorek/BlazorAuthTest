using MediatR;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.Products.ProductCosts.Commands.EditProductCost;

public class EditProductCostCommand : IRequest
{
    public int Id { get; set; }

    [Display(Name = "Okres")]
    public int AccountingPeriodId { get; set; }

    [Display(Name = "Produkt")]
    public int ProductId { get; set; }

    [Display(Name = "Waluta")]
    public int CurrencyId { get; set; }


    [Display(Name = "Koszt Całkowity")]
    public decimal Cost { get; set; }

    [Display(Name = "Koszt Robocizny")]
    public decimal LabourCost { get; set; }

    [Display(Name = "Koszt Mareriałów")]
    public decimal MaterialCost { get; set; }

    [Display(Name = "Koszt Narzutu")]
    public decimal MarkupCost { get; set; }

    [Display(Name = "komentarz")]
    public string? Description { get; set; }

    [Display(Name = "Szacowany Koszt Całkowity")]
    public decimal EstimatedCost { get; set; }

    [Display(Name = "Szacowany Koszt Robocizny")]
    public decimal EstimatedLabourCost { get; set; }

    [Display(Name = "Szacowany Koszt Mareriałów")]
    public decimal EstimatedMaterialCost { get; set; }

    [Display(Name = "Szacowany Koszt Narzutu")]
    public decimal EstimatedMarkupCost { get; set; }

}
