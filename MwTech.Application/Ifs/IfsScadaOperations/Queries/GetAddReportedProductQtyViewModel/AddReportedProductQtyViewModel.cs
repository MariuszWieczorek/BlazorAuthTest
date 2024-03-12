using MwTech.Application.Ifs.IfsScadaOperations.Commands.AddReportedProductQty;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Ifs.IfsScadaOperations.Queries.GetAddReportedProductQtyViewModel;

public class AddReportedProductQtyViewModel
{
    public AddReportedProductQtyCommand AddReportedProductQtyCommand { get; set; }
    public IfsScadaOperation IfsScadaOperation { get; set; }
}
