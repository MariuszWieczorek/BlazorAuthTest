using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Queries.GetAddProductPropertiesVersionViewModel;

public class GetAddProductPropertiesVersionViewModelQuery : IRequest<AddProductPropertiesVersionViewModel>
{
    public int ProductId { get; set; }
    public string? Tab { get; set; }
}
