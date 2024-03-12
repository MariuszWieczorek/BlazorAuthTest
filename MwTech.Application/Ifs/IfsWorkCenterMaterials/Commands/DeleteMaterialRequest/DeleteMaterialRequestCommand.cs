using MediatR;

namespace MwTech.Application.Ifs.IfsWorkCenterMaterials.Commands.DeleteMaterialRequest;

public class DeleteMaterialRequestCommand : IRequest
{
    public int ReqId { get; set; }

}
