using MediatR;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Commands.ExecuteMaterialRequest;

public class ExecuteMaterialRequestCommand : IRequest
{
    public int ReqId { get; set; }

}
