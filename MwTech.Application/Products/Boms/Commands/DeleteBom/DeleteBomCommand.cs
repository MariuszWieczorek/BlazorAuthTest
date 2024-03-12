using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.Boms.Commands.DeleteBom;

public class DeleteBomCommand : IRequest
{
    public int Id { get; set; }

       [Display(Name = "Zestaw")]
    public int ProductId { get; set; }


    [Display(Name = "Zestaw Wersja")]
    public int ProductVersionId { get; set; }

}
