using MwTech.Application.AccountingPeriods.Commands.EditAccountingPeriod;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.AccountingPeriods.Queries.GetEditAccountingPeriodViewModel;

public class EditAccountingPeriodViewModel
{
    public EditAccountingPeriodCommand EditAccountingPeriodCommand { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
    public IEnumerable<Unit> Units { get; set; }

}
