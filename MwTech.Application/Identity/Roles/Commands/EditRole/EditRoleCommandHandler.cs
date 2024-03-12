using MwTech.Application.Common.Interfaces;
using MwTech.Application.Identity.Roles.Queries.GetRoles;
using MediatR;


namespace MwTech.Application.Identity.Roles.Commands.EditRole;
public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand>
{
    private readonly IRoleManagerService _roleManagerService;

    public EditRoleCommandHandler(IRoleManagerService roleManagerService)
    {
        _roleManagerService = roleManagerService;
    }

    public async Task Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleManagerService.UpdateAsync(new RoleDto
        {
            Id = request.Id,
            Name = request.Name,
            IdsToAdd = request.IdsToAdd,
            IdsToDelete = request.IdsToDelete,
        });



        return;
    }
}
