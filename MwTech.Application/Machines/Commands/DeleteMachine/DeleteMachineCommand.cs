using MediatR;

namespace MwTech.Application.Machines.Commands.DeleteMachine;

public class DeleteMachineCommand : IRequest
{
    public int Id { get; set; }
}
