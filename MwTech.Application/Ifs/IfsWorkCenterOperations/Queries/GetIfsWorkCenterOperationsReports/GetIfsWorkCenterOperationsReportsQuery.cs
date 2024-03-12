using MediatR;
using MwTech.Application.Ifs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsReports;

public class GetIfsWorkCenterOperationsReportsQuery : IRequest<IfsWorkCenterOperationsReportsViewModel>
{
    public IfsWorkCenterOperationsReportsFilter IfsWorkCenterOperationsReportsFilter { get; set; }
}
