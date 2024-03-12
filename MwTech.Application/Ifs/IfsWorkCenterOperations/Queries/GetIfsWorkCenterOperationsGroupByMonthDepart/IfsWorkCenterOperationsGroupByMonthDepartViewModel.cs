using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsGroupByMonthDepart;

public class IfsWorkCenterOperationsGroupByMonthDepartViewModel
{
    public IfsWorkCenterOperationsReportsFilter IfsWorkCenterOperationsGroupByMonthDepartFilter { get; set; }
    public List<IfsWorkCenterOperationsByMonthDepart> IfsWorkCentersOperationsGroupByMonthDepart { get; set; }
    public IfsWorkCenterOperationsSummary IfsWorkCenterOperationsSummary { get; set; }
}
