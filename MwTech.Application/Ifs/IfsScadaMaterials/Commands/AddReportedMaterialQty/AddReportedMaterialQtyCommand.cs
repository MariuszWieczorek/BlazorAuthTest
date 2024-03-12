using MediatR;

namespace MwTech.Application.Ifs.IfsScadaMaterials.Commands.AddReportedMaterialQty;

public class AddReportedMaterialQtyCommand : IRequest
{
    public int OP_ID { get; set; }
    public string COMPONENT_PART_NO { get; set; }
    public decimal QTY_ISSUED { get; set; }
    public string LOT_BATCH_NO { get; set; }
    public string HANDLING_UNIT_ID { get; set; }

}
