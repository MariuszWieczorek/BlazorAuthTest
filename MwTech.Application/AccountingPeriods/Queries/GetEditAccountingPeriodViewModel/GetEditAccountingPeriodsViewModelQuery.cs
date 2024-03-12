using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.AccountingPeriods.Queries.GetEditAccountingPeriodViewModel;

public class GetEditAccountingPeriodViewModelQuery : IRequest<EditAccountingPeriodViewModel>
{
    public int Id { get; set; }
}
