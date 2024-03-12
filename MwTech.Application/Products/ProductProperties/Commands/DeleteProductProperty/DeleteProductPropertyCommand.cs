using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.ProductProperties.Commands.DeleteProductProperty;

public class DeleteProductPropertyCommand : IRequest
{
    public int Id { get; set; }

    [Display(Name = "Wersja Atrybutów")]
    public int ProductPropertiesVersionId { get; set; }

}
