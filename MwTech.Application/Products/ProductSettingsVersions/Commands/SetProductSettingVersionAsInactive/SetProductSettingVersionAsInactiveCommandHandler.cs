using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.ProductSettingsVersions.Commands.SetProductSettingVersionAsInactive;


public class SetProductSettingVersionAsInactiveCommandHandler : IRequestHandler<SetProductSettingVersionAsInactiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductSettingVersionAsInactiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetProductSettingVersionAsInactiveCommand request, CancellationToken cancellationToken)
    {
        var settingsVersion = _context.ProductSettingVersions
            .SingleOrDefault(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);
                        
        settingsVersion.IsActive = false;

        await _context.SaveChangesAsync();

        return;
    }
}
