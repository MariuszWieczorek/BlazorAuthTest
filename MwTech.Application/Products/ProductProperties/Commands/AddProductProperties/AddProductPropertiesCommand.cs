using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MwTech.Application.Products.ProductProperties.Commands.AddProductProperties;

public class AddProductPropertiesCommand : IRequest
{
    public int ProductPropertiesVersionId { get; set; }

}
