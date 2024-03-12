using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Queries.GetIfsWorkCenterMaterials;

public class IfsWorkCenterMaterialsViewModel
{
    public IfsWorkCenterMaterialFilter IfsWorkCenterMaterialFilter { get; set; }
    public List<IfsWorkCenterMaterial> IfsWorkCenterMaterials { get; set; }
}
