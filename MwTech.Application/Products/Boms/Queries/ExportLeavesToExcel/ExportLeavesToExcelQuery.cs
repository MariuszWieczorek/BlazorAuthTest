using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Boms.Queries.ExportLeavesToExcel;

public class ExportLeavesToExcelQuery : IRequest<string>
{
    public int ProductId { get; set; }
    public int ProductVersionId { get; set; }

}