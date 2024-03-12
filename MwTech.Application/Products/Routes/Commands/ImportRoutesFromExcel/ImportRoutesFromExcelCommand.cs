using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Routes.Commands.ImportRoutesFromExcel;

public class ImportRoutesFromExcelCommand : IRequest
{
    public string FileName { get; set; }
}
