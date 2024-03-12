using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.AcceptProductSettingVersionLevel3;

public class AcceptProductSettingVersionLevel3Command : IRequest
{
    public int ProductSettingVersionId { get; set; }
    public int ProductId { get; set; }

}
