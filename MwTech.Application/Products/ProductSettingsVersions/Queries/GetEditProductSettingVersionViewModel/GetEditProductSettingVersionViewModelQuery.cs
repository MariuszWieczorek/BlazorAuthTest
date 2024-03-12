using MediatR;
using MwTech.Application.Resources.Queries.GetResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Queries.GetEditProductSettingVersionViewModel;

public class GetEditProductSettingVersionViewModelQuery : IRequest<EditProductSettingVersionViewModel>
{
    public int ProductId { get; set; }
    public int ProductSettingVersionId { get; set; }
    public ResourceFilter ResourceFilter { get; set; }
    public string? Tab { get; set; }
    public string? ReturnAddress { get; set; }
    public string? Anchor { get; set; }
}
