using MwTech.Application.Ifs.IfsScadaMaterials.Commands.AddReportedMaterialQty;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Ifs.IfsScadaMaterials.Queries.GetAddReportedMaterialQtyViewModel;

public class AddReportedMaterialQtyViewModel
{
    public AddReportedMaterialQtyCommand AddReportedMaterialQtyCommand { get; set; }
    public IfsScadaMaterial IfsScadaMaterial { get; set; }
}
