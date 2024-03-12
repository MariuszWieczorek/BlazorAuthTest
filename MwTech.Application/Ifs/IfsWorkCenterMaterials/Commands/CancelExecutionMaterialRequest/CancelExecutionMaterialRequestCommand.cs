using MediatR;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Commands.CancelExecutionMaterialRequest;

public class CancelExecutionMaterialRequestCommand : IRequest
{
    public int ReqId { get; set; }

}
