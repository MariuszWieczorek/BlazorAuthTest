using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetProductTkw;

public class GetProductTkwQuery : IRequest<ProductTkwViewModel>
{
    public int ProductId { get; set; }
    public int ProductVersionId { get; set; }
}
