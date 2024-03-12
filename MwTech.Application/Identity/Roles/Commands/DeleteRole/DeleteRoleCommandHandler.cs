﻿using MwTech.Application.Common.Interfaces;
using MediatR;


namespace MwTech.Application.Identity.Roles.Commands.DeleteRole;
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IRoleManagerService _roleManagerService;

    public DeleteRoleCommandHandler(IRoleManagerService roleManagerService)
    {
        _roleManagerService = roleManagerService;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleManagerService.DeleteAsync(request.Id);

        return;
    }
}
