using MediatR;
using MwTech.Application.Resources.Queries.GetResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Resources.Queries.GetEditResourceViewModel;

public class GetEditResourceViewModelQuery : IRequest<EditResourceViewModel>
{
    public int Id { get; set; }
    public ResourceFilter ResourceFilter { get; set; }
}
