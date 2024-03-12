using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Commands.SetProductVersionAsInactive;

public class SetProductVersionAsInactiveCommand : IRequest
{
    public int ProductVersionId { get; set; }
    public int ProductId { get; set; }
}
