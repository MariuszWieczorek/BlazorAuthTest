using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsGroupByProduct;

public class GetIfsWorkCenterOperationsGroupByProductQuery : IRequest<IfsWorkCenterOperationsGroupByProductViewModel>
{
    public string WorkCenterNo { get; set; }
}
