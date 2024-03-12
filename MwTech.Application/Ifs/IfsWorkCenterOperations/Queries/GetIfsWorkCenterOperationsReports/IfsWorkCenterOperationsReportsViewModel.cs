using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsReports;

public class IfsWorkCenterOperationsReportsViewModel
{
    public IfsWorkCenterOperationsReportsFilter IfsWorkCenterOperationsReportsFilter { get; set; }
    public List<IfsWorkCenterOperationsReport> IfsWorkCentersOperationsReports { get; set; }
    public IfsWorkCenterOperationsSummary IfsWorkCenterOperationsSummary { get; set; }
}
