using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.ProductSettingsVersions.Commands.SetProductSettingVersionAsActive;


public class SetProductSettingVersionAsActiveCommandHandler : IRequestHandler<SetProductSettingVersionAsActiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductSettingVersionAsActiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetProductSettingVersionAsActiveCommand request, CancellationToken cancellationToken)
    {
        var settingsVersion = _context.ProductSettingVersions
            .SingleOrDefault(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);
                        
        settingsVersion.IsActive = true;

        await _context.SaveChangesAsync();

        return;
    }
}
