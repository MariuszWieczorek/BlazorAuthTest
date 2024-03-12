using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.SetProductPropertiesVersionAsActive;

public class SetProductPropertiesVersionAsActiveCommand : IRequest
{
    public int ProductPropertiesVersionId { get; set; }
    public int ProductId { get; set; }
}
