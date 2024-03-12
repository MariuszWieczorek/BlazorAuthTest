using MediatR;

namespace MwTech.Application.AccountingPeriods.Commands.DeleteAccountingPeriod;

public class DeleteAccountingPeriodCommand : IRequest
{
    public int Id { get; set; }
}
