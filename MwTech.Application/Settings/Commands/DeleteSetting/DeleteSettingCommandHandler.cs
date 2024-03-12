using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Settings.Commands.DeleteSetting;

public class DeleteSettingCommandHandler : IRequestHandler<DeleteSettingCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSettingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSettingCommand request, CancellationToken cancellationToken)
    {

        var settingToDelete = await _context.Settings
            .SingleOrDefaultAsync(x => x.Id == request.Id);
    
        _context.Settings.Remove(settingToDelete);
        
        await _context.SaveChangesAsync();

        return;

    }
}
