using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Operations.Queries.GetOperations;

public class GetOperationsQuery : IRequest<GetOperationsViewModel>
{
    public OperationFilter OperationFilter { get; set; }
}
