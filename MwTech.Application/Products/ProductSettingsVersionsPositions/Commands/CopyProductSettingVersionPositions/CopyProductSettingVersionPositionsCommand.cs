using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.CopyProductSettingVersionPositions;

public class CopyProductSettingVersionPositionsCommand : IRequest
{
    [Display(Name = "Wersja Ustawień")]
    public int ProductSettingVersionId { get; set; }

    [Display(Name = "Produkt")]
    public int ProductId { get; set; }

    [Display(Name = "Indeks Produktu")]
    public string ProductNumber { get; set; }

    [Display(Name = "Nazwa Produktu")]
    public string ProductName { get; set; }



    [Display(Name = "Wersja Źródłowa - Indeks")]
    public string SourceProductNumber { get; set; }

    [Display(Name = "Wersja Źródłowa")]
    public int SourceProductSettingVersionId { get; set; }

    [Display(Name = "Nazwa Wersji Źródłowej")]
    public string SourceProductSettingVersionName { get; set; }

    [Display(Name = "Numer wersji Źródłowej")]
    public string SourceProductSettingVersionNumber { get; set; }


}
