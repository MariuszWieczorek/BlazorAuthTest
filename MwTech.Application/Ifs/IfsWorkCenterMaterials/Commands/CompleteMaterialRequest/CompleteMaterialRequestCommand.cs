using MediatR;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Commands.CompleteMaterialRequest;

public class CompleteMaterialRequestCommand : IRequest
{
    public int ReqId { get; set; }

}
