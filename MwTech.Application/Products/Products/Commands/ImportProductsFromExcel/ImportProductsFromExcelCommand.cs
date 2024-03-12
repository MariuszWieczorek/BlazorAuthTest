using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Commands.ImportProductsFromExcel;

public class ImportProductsFromExcelCommand : IRequest
{
    public string FileName { get; set; }
}
