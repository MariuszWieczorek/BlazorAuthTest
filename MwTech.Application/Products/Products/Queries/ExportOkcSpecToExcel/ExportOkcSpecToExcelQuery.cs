using MediatR;
using MwTech.Application.Products.Products.Queries.GetOkcSpec;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.OkcSpec.Queries.ExportOkcSpecToExcel;

public class ExportOkcSpecToExcelQuery : IRequest<string>
{
    public IEnumerable<OkcSpecDto> OkcSpec { get; set; }
}
