using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.ProductSettingsVersions.Commands.GenerateProductsSettingsCsv;

public class GenerateProductsSettingsCsvCommand : IRequest
{
    public string ProductCategoryNumber { get; set; }
}
