using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.ProductSettingsVersions.Commands.SetProductSettingVersionAsDefault;


public class SetProductSettingVersionAsDefaultCommandHandler : IRequestHandler<SetProductSettingVersionAsDefaultCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductSettingVersionAsDefaultCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetProductSettingVersionAsDefaultCommand request, CancellationToken cancellationToken)
    {
        var settingsVersions = _context.ProductSettingVersions
            .Where(x => x.ProductId == request.ProductId
             && x.MachineId == request.MachineId
            );
                        
        foreach (var item in settingsVersions)
        {
            item.DefaultVersion = (item.Id == request.ProductSettingVersionId);
            if (item.Id == request.ProductSettingVersionId)
            {
                item.IsActive = true;
            }
        }

        await _context.SaveChangesAsync();

        return;
    }
}
