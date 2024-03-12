using MediatR;

namespace MwTech.Application.Products.Products.Commands.CopyProduct;

public class CopyProductCommand : IRequest
{
    public int Id { get; set; }
}
