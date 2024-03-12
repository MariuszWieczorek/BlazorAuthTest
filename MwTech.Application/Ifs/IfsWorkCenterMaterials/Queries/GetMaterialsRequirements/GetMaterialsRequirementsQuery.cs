using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Queries.GetMaterialsRequirements;

public class GetMaterialsRequirementsQuery : IRequest<MaterialsRequirementsViewModel>
{
    public MaterialRequirementFilter MaterialRequirementFilter { get; set; }
}
