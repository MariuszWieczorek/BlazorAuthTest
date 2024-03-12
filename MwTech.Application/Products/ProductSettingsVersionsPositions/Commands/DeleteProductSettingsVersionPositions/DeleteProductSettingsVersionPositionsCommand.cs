using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.DeleteProductSettingsVersionPositions;

public class DeleteProductSettingsVersionPositionsCommand : IRequest
{

    [Display(Name = "Produkt")]
    public int ProductId { get; set; }

    [Display(Name = "Wersja Produktu")]
    public int ProductSettingVersionId { get; set; }

}
