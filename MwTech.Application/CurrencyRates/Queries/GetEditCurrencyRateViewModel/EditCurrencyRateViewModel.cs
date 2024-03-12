using MwTech.Application.CurrencyRates.Commands.EditCurrencyRate;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.CurrencyRates.Queries.GetEditCurrencyRateViewModel;

public class EditCurrencyRateViewModel
{
    public EditCurrencyRateCommand EditCurrencyRateCommand { get; set; }
    public IEnumerable<AccountingPeriod> AccountingPeriods { get; set; }
    public IEnumerable<Currency> Currencies { get; set; }

}
