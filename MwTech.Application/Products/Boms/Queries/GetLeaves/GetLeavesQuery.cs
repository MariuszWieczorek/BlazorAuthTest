using MediatR;

namespace MwTech.Application.Products.Boms.Queries.GetLeaves;

public class GetLeavesQuery : IRequest<GetLeavesViewModel>
{
    public int ProductId { get; set; }
    public int ProductVersionId { get; set; }
}
