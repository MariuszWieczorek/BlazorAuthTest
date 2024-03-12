using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.WithdrawLevel1ProductSettingVersionAcceptance;

public class WithdrawLevel1ProductSettingVersionAcceptanceCommand : IRequest
{
    public int ProductSettingVersionId { get; set; }
    public int ProductId { get; set; }
}
