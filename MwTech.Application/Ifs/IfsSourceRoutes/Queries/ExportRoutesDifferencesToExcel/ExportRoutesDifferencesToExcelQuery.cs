using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesDifferencesToExcel;

public class ExportRoutesDifferencesToExcelQuery : IRequest<string>
{
    public IEnumerable<ComparedRouteIfsVsMwTech> ComparedRoutes { get; set; }
}
