using MwTech.Application.Products.ProductCosts.Commands.AddProductCost;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Queries.GetAddProductCostViewModel;

public class AddProductCostViewModel
{
    public AddProductCostCommand AddProductCostCommand { get; set; }
    public List<AccountingPeriod> AccountingPeriods { get; set; }
    public List<Currency> Currencies { get; set; }
    public List<Product> Products { get; set; }

}
