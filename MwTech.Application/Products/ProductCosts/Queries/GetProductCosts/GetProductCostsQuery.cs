using MediatR;
using MwTech.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Queries.GetProductCosts;

public class GetProductCostsQuery : IRequest<ProductCostsViewModel>
{
    public ProductCostFilter ProductCostFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
}
