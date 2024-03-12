using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.CurrencyRates.Queries.GetAddCurrencyRateViewModel;

public class GetAddCurrencyRateViewModelQuery : IRequest<AddCurrencyRateViewModel>
{
    public int CurrencyId { get; set; }
    public int PeriodId { get; set; }

}
