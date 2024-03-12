using MediatR;

namespace MwTech.Application.AccountingPeriods.Commands.SetDefaultAccountingPeriod;

public class SetDefaultAccountingPeriodCommand : IRequest
{
    public int Id { get; set; }
}
