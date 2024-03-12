using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.ProductSettingsVersions.Commands.GenerateSingleProductSettingsCsv;

public class GenerateSingleProductSettingsCsvCommand : IRequest
{
    public int ProductId { get; set; }
    public int ProductSettingsVersionId { get; set; }
}
