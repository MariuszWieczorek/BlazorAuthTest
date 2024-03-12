using MediatR;

namespace MwTech.Application.Operations.Commands.DeleteOperation;

public class DeleteOperationCommand : IRequest
{
    public int Id { get; set; }
}
