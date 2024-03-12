using MediatR;
using MwTech.Application.Products.Common;

namespace MwTech.Application.Products.Boms.Queries.GetEditBomViewModel;

public class GetEditBomViewModelQuery : IRequest<EditBomViewModel>
{
    public int Id { get; set; }
    public int ProductVersionId { get; set; }
    public int ProductId { get; set; }
    public string Tab { get; set; }
    public ProductFilter ProductFilter { get; set; }

}
