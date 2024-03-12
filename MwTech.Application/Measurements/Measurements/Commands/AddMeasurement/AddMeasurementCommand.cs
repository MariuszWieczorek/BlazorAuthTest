using MediatR;
using MwTech.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace MwTech.Application.Measurements.Measurements.Commands.AddMeasurement;

public class AddMeasurementCommand : IRequest
{

    [Display(Name = "Kod Ean13")]
    public string CodeEan13 { get; set; }

    [Required(ErrorMessage = "Pole 'Produkt' jest wymagane")]
    [Display(Name = "Produkt")]
    public int? ProductId { get; set; }


    [Required(ErrorMessage = "Pole 'Produkt' jest wymagane")]
    [Display(Name = "Produkt")]
    public string ProductNumber { get; set; }


    [Required(ErrorMessage = "Pole 'Produkt' jest wymagane")]
    [Display(Name = "Produkt")]
    public string ProductName { get; set; }


    [Display(Name = "Wartość Wzorcowa")]
    public decimal? PorductWeightInKg { get; set; }

    [Display(Name = "Wartość Min")]
    public decimal? PorductMinWeightInKg { get; set; }

    [Display(Name = "Wartość Max")]
    public decimal? PorductMaxWeightInKg { get; set; }


    [Display(Name = "Wartość Zważona")]
    [Required(ErrorMessage = "Pole 'Wartość' jest wymagane")]
    public decimal? Value { get; set; }
    
}
