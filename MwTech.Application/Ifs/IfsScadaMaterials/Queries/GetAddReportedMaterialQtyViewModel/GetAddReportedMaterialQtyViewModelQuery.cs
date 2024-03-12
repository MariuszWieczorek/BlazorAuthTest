using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsScadaMaterials.Queries.GetAddReportedMaterialQtyViewModel;

public class GetAddReportedMaterialQtyViewModelQuery : IRequest<AddReportedMaterialQtyViewModel>
{
    public int OP_ID { get; set; }
    public string COMPONENT_PART_NO { get; set; }
}
