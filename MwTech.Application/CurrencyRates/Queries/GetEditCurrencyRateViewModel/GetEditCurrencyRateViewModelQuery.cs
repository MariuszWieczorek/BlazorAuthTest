using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.CurrencyRates.Queries.GetEditCurrencyRateViewModel;

public class GetEditCurrencyRateViewModelQuery : IRequest<EditCurrencyRateViewModel>
{
    public int Id { get; set; }
}
