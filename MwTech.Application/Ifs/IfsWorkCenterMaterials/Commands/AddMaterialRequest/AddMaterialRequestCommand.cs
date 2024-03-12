using MediatR;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Commands.AddMaterialRequest;

public class AddMaterialRequestCommand : IRequest
{
    public string OrderNo { get; set; }
    public string PartNo { get; set; }
    public string WorkCenterNo { get; set; }
    public decimal QtyRequired { get; set; }
    public string SourceLocation { get; set; }

}
