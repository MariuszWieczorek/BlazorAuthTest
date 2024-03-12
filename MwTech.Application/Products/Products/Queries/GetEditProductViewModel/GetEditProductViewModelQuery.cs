using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetEditProductViewModel;

public class GetEditProductViewModelQuery : IRequest<EditProductViewModel>
{
    public int ProductId { get; set; }
    public string? Tab { get; set; }
}
