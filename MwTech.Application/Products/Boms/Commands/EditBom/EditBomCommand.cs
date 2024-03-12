using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.Boms.Commands.EditBom;

public class EditBomCommand : IRequest
{
    public int Id { get; set; }

    [Display(Name = "Lp")]
    public int OrdinalNumber { get; set; }


    [Display(Name = "Zestaw")]
    public int SetId { get; set; }


    [Display(Name = "Zestaw Wersja")]
    public int SetVersionId { get; set; }


    [Display(Name = "Część")]
    public int PartId { get; set; }

    public Product Part { get; set; }

    [Display(Name = "ilość")]
    public decimal PartQty { get; set; }

    [Display(Name = "długość części")]
    public decimal PartLength { get; set; }
    

    [Display(Name = "zużywane")]
    public bool OnProductionOrder { get; set; }

    [Display(Name = "nie wliczaj do TKW")]
    public bool DoNotIncludeInTkw { get; set; }

    [Display(Name = "nie wliczaj do Wagi")]
    public bool DoNotIncludeInWeight { get; set; }

    [Display(Name = "nie wysyłaj do IFS")]
    public bool DoNotExportToIfs { get; set; }

    [Display(Name = "nadmiar %")]
    public decimal Excess { get; set; }

    [Display(Name = "odpad %")]
    public decimal Scrap { get; set; }


    [Display(Name = "Warstwa")]
    public int Layer { get; set; }
}
