using MediatR;
using MwTech.Application.Ifs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsGroupByMonthDepart;

public class GetIfsWorkCenterOperationsGroupByMonthDepartQuery : IRequest<IfsWorkCenterOperationsGroupByMonthDepartViewModel>
{
    public IfsWorkCenterOperationsReportsFilter IfsWorkCenterOperationsReportsFilter { get; set; }
}
