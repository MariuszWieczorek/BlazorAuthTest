using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsGroupByProduct;

public class IfsWorkCenterOperationsGroupByProductViewModel
{
    public string WorkCenterNo { get; set; }
    public List<IfsWorkCenterOperationsGroupByProduct> IfsWorkCenterOperationsGroupByProduct { get; set; }
}
