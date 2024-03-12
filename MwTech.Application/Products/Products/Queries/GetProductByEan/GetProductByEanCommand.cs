using MediatR;
using MwTech.Application.Products.Common;

namespace MwTech.Application.Products.Products.Queries.GetSimpleProductWithWeightByEan;

public class GetSimpleProductWithWeightByEanCommand : IRequest<SimpleProductWithWeight>
{
    public string Ean13Code { get; set; }
}
