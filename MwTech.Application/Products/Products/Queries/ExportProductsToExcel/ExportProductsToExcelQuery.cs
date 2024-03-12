using MediatR;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.ExportProductsToExcel;

public class ExportProductsToExcelQuery : IRequest<string>
{
    public IEnumerable<Product> Products { get; set; }
}
