using MwTech.Domain.Entities;
namespace MwTech.Application.AccountingPeriods.Queries.GetAccountingPeriods;

public class AccountingPeriodsViewModel
{
    public IEnumerable<AccountingPeriod> AccountingPeriods { get; set; }
    public AccountingPeriodFilter AccountingPeriodFilter { get; set; }

}
