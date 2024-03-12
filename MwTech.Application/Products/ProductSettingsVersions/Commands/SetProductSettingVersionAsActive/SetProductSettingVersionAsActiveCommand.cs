using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.ProductSettingsVersions.Commands.SetProductSettingVersionAsActive;

public class SetProductSettingVersionAsActiveCommand : IRequest
{
    public int ProductSettingVersionId { get; set; }
    public int ProductId { get; set; }
    public int MachineId { get; set; }
}
