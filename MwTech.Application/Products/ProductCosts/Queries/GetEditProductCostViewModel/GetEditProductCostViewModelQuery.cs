using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Queries.GetEditProductCostViewModel;

public class GetEditProductCostViewModelQuery : IRequest<EditProductCostViewModel>
{
    public int Id { get; set; }
}
