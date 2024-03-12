using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Queries.GetAddProductVersionViewModel;

public class GetAddProductVersionViewModelQuery : IRequest<AddProductVersionViewModel>
{
    public int ProductId { get; set; }
    public string? Tab { get; set; }
}
