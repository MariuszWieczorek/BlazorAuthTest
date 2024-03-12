using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.AcceptProductSettingVersionLevel1;

public class AcceptProductSettingVersionLevel1Command : IRequest
{
    public int ProductSettingVersionId { get; set; }
    public int ProductId { get; set; }
}
