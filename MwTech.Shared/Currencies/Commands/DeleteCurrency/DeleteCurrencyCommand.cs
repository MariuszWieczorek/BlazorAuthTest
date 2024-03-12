using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Shared.Currencies.Commands.DeleteCurrency;

public class DeleteCurrencyCommand : IRequest
{
    public int Id { get; set; }
}
