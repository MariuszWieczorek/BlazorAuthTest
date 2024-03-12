using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Queries.GetAddProductCostViewModel;

public class GetAddProductCostViewModelQuery : IRequest<AddProductCostViewModel>
{
    public int CurrencyId { get; set; }
    public int PeriodId { get; set; }
    public int ProductId { get; set; }
}
