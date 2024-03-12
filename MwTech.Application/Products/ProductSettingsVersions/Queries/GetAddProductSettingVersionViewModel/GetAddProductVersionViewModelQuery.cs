using MediatR;
using MwTech.Application.Resources.Queries.GetResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Queries.GetAddProductSettingVersionViewModel;

public class GetAddProductSettingVersionViewModelQuery : IRequest<AddProductSettingVersionViewModel>
{
    public int ProductId { get; set; }
    public int MachineCategoryId { get; set; }
    public int MachineId { get; set; }
    public int WorkCenterId { get; set; }
    public ResourceFilter ResourceFilter { get; set; }
    public string? Tab { get; set; }
}
