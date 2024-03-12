﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.ImportProductSettingVersionPositionFromExcel;

public class ImportSettingsFromExcelCommand : IRequest
{
    public string FileName { get; set; }
}
