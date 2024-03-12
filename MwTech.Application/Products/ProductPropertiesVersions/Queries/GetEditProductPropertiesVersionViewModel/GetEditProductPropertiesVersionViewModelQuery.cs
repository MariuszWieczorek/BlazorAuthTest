using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Queries.GetEditProductPropertiesVersionViewModel;

public class GetEditProductPropertiesVersionViewModelQuery : IRequest<EditProductPropertiesVersionViewModel>
{
    public int ProductPropertiesVersionId { get; set; }
    public int ProductId { get; set; }
    public string? Tab { get; set; }
    public string? ReturnAddress { get; set; }
    public string? Anchor { get; set; }
}
