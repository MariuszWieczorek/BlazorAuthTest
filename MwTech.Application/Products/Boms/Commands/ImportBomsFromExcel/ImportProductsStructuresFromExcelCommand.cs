using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Boms.Commands.ImportBomsFromExcel;

public class ImportBomsFromExcelCommand : IRequest
{
    public string FileName { get; set; }
}
