using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Queries.GetEditProductVersionViewModel;

public class GetEditProductVersionViewModelQuery : IRequest<EditProductVersionViewModel>
{
    public int ProductVersionId { get; set; }
    public int ProductId { get; set; }
    public string? Tab { get; set; }
}
