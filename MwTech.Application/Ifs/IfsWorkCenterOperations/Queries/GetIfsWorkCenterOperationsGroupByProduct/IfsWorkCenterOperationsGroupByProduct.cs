using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsGroupByProduct;

public class IfsWorkCenterOperationsGroupByProduct
{
    public string ProductNumber { get; set; }
    public List<IfsWorkCenterOperation> IfsWorkCenterOperations { get; set; }
    public List<ProductProperty> Params { get; set; } = new List<ProductProperty>();
    public List<ComponentUsage> ComponentUsages { get; set; } = new List<ComponentUsage>();

}
