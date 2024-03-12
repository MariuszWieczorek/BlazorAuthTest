using MediatR;

namespace MwTech.Application.Products.ProductCosts.Commands.DeleteProductCost;

public class DeleteProductCostCommand : IRequest
{
    public int Id { get; set; }
}
