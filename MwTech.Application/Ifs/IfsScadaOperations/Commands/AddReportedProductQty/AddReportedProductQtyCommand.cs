using MediatR;

namespace MwTech.Application.Ifs.IfsScadaOperations.Commands.AddReportedProductQty;

public class AddReportedProductQtyCommand : IRequest
{
    public int OP_ID { get; set; }
    public decimal QTY_REPORTED { get; set; }
    public decimal TIME_CONSUMED { get; set; }
    public string LOT_BATCH_NO { get; set; }
    public string HANDLING_UNIT_ID { get; set; }

}
