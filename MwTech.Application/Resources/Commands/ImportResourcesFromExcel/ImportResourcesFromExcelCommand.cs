﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Resources.Commands.ImportResourcesFromExcel;

public class ImportResourcesFromExcelCommand : IRequest
{
    public string FileName { get; set; }
}
