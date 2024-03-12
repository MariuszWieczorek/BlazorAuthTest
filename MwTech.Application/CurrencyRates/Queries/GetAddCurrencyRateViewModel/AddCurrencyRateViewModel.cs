using MwTech.Application.CurrencyRates.Commands.AddCurrencyRate;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.CurrencyRates.Queries.GetAddCurrencyRateViewModel;

public class AddCurrencyRateViewModel
{
    public AddCurrencyRateCommand AddCurrencyRateCommand { get; set; }
    public List<AccountingPeriod> AccountingPeriods { get; set; }
    public List<Currency> Currencies { get; set; }

}
