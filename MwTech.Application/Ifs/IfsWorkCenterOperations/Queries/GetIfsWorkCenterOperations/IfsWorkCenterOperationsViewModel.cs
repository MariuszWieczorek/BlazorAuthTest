using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperations;

public class IfsWorkCenterOperationsViewModel
{
    public string WorkCenterNo { get; set; }
    public List<IfsWorkCenterOperation> IfsWorkCenterOperations { get; set; }

    public IfsWorkCenterOperationsReportsFilter Filter { get; set; }
    public DateTime PrintTime { get; set; }
}
