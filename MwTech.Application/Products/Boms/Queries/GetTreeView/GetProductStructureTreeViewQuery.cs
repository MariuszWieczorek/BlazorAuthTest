using MediatR;

namespace MwTech.Application.Products.Boms.Queries.GetBomTreeView;

public class GetBomTreeViewQuery : IRequest<GetBomTreeViewViewModel>
{
    public int ProductId { get; set; }
    public int ProductVersionId { get; set; }
}
