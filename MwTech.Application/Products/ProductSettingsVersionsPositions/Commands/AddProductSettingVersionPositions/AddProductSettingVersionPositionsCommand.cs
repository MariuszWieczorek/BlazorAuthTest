using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.AddProductSettingVersionPositions;

public class AddProductSettingVersionPositionsCommand : IRequest
{
    [Display(Name = "Wersja Ustawień")]
    public int ProductSettingVersionId { get; set; }

    [Display(Name = "Produkt")]
    public int ProductId { get; set; }
    
}
