using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductProperties.Commands.ImportProductPropertiesFromExcel;

public class ImportProductPropertiesFromExcelCommand : IRequest
{
    public string FileName { get; set; }
}
