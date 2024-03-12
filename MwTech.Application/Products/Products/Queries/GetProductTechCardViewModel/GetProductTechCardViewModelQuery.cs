using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetProductTechCardViewModel;

public class GetProductTechCardViewModelQuery : IRequest<ProductTechCardViewModel>
{
    public int? ProductId { get; set; }
    public string? ProductNumber { get; set; }
    public string? Tab { get; set; }
}
