using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Commands.ImportProductsCostFromExcel;

public class ImportProductsCostFromExcelCommand : IRequest
{
    public string FileName { get; set; }
}
