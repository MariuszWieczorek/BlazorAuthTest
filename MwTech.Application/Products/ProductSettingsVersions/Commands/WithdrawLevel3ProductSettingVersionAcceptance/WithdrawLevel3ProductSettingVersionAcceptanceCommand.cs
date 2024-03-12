using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.WithdrawLevel3ProductSettingVersionAcceptance;

public class WithdrawLevel3ProductSettingVersionAcceptanceCommand : IRequest
{
    public int ProductSettingVersionId { get; set; }
    public int ProductId { get; set; }
}
