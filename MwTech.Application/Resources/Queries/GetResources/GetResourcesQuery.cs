using MediatR;
using MwTech.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Resources.Queries.GetResources;

public class GetResourcesQuery : IRequest<GetResourcesViewModel>
{
    public ResourceFilter ResourceFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public int Id { get; set; }
}
