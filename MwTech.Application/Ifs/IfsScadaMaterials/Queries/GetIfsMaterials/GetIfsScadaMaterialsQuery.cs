using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsScadaMaterials.Queries.GetIfsMaterials;

public class GetIfsScadaMaterialsQuery : IRequest<IfsScadaMaterialsViewModel>
{
    public IfsScadaMaterialFilter IfsScadaMaterialFilter { get; set; }
}
