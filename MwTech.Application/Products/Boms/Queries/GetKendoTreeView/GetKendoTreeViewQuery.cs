using MediatR;

namespace MwTech.Application.Products.Boms.Queries.GetKendoTreeView;

public class GetKendoTreeViewQuery : IRequest<KendoTreeViewViewModel>
{
    public int ProductId { get; set; }
    public int ProductVersionId { get; set; }
}
