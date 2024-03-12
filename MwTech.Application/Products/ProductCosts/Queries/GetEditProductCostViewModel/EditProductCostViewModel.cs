using MwTech.Application.Products.ProductCosts.Commands.EditProductCost;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Queries.GetEditProductCostViewModel;

public class EditProductCostViewModel
{
    public EditProductCostCommand EditProductCostCommand { get; set; }
    public IEnumerable<AccountingPeriod> AccountingPeriods { get; set; }
    public IEnumerable<Currency> Currencies { get; set; }

}
