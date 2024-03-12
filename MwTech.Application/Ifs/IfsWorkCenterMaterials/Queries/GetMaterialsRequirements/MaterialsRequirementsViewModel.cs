using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Queries.GetMaterialsRequirements;

public class MaterialsRequirementsViewModel
{
    public MaterialRequirementFilter MaterialRequirementFilter { get; set; }
    public List<IfsWorkCenterMaterialRequest> IfsWorkCenterMaterialRequests { get; set; }
}
