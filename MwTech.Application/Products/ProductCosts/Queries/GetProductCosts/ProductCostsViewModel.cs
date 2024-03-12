using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;
namespace MwTech.Application.Products.ProductCosts.Queries.GetProductCosts;

public class ProductCostsViewModel
{
    public IEnumerable<ProductCostDto> ProductCosts { get; set; }
    public ProductCostFilter ProductCostFilter { get; set; }
    public IEnumerable<AccountingPeriod> AccountingPeriods { get; set; }
    public IEnumerable<Currency> Currencies { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
