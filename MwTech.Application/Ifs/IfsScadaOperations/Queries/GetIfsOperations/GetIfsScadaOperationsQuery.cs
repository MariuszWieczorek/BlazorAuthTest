using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsScadaOperations.Queries.GetIfsOperations;

public class GetIfsScadaOperationsQuery : IRequest<IfsScadaOperationsViewModel>
{
    public IfsScadaOperationFilter IfsScadaOperationFilter { get; set; }
}
