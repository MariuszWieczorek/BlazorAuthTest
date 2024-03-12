﻿using MwTech.Application.Common.Interfaces;
using MediatR;

namespace MwTech.Application.Identity.Roles.Commands.AddRole;
public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand>
{
    private readonly IRoleManagerService _roleManagerService;

    public AddRoleCommandHandler(IRoleManagerService roleManagerService)
    {
        _roleManagerService = roleManagerService;
    }

    public async Task Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        await _roleManagerService.CreateAsync(request.Name);

        return;
    }

}
