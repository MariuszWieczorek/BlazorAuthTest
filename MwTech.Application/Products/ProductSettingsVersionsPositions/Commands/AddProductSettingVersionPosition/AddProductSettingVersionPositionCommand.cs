using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.AddProductSettingVersionPosition;

public class AddProductSettingVersionPositionCommand : IRequest
{


    [Display(Name = "Produkt")]
    public int ProductId { get; set; }

    [Display(Name = "Wersja Ustawień")]
    public int ProductSettingVersionId { get; set; }

    [Display(Name = "Wersja Ustawień")]
    public ProductSettingVersion? ProductSettingVersion { get; set; }


    [Display(Name = "Ustawienie")]
    public int SettingId { get; set; }

    [Display(Name = "Ustawienie")]
    public Setting? Setting { get; set; }


    [Display(Name = "Wartość Tekstowa")]
    public string? Text { get; set; }

    [Display(Name = "Wartość")]
    public decimal Value { get; set; }

    [Display(Name = "Wartość Min")]
    public decimal? MinValue { get; set; }

    [Display(Name = "Wartość Max")]
    public decimal? MaxValue { get; set; }

    [Display(Name = "Opis")]
    public string? Description { get; set; }

    [Display(Name = "Aktywne")]
    public bool IsActive { get; set; } = true;
}
