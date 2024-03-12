using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Commands.ImportDefaultRouteVersion;

public class ImportDefaultRouteVersionCommand : IRequest
{
    public string FileName { get; set; }
}
