using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.CurrencyRates.Queries.GetCurrencyRates;

public class GetCurrencyRatesQuery : IRequest<CurrencyRatesViewModel>
{
    public CurrencyRateFilter CurrencyRateFilter { get; set; }
}
