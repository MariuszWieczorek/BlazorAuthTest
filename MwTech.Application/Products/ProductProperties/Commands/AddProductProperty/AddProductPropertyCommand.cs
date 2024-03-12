using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.ProductProperties.Commands.AddProductProperty;

public class AddProductPropertyCommand : IRequest
{

    [Display(Name = "Cecha")]
    public Property? Property { get; set; }

    [Display(Name = "Cecha")]
    public int PropertyId { get; set; }


    [Display(Name = "Product Id")]
    public int ProductId { get; set; }

    [Display(Name = "Wersja Produktu")]
    public int ProductPropertiesVersionId { get; set; }

    [Display(Name = "Wartość Cel")]
    public decimal? Value { get; set; }

    [Display(Name = "Wartość Min")]
    public decimal? MinValue { get; set; }

    [Display(Name = "Wartość Max")]
    public decimal? MaxValue { get; set; }

    [Display(Name = "Tekst")]
    public string? Text { get; set; }
}
