using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.DeleteProductSettingVersion;

public class DeleteProductSettingVersionCommandHandler : IRequestHandler<DeleteProductSettingVersionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductSettingVersionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteProductSettingVersionCommand request, CancellationToken cancellationToken)
    {

        var productSettingVersionToDelete = await _context.ProductSettingVersions
            .SingleOrDefaultAsync(x => x.Id == request.ProductSettingVersionId);
        
        
        // usuwamy pozycje
        var settingPositionsToDelete = _context.ProductSettingVersionPositions
                .Where(x => x.ProductSettingVersionId == request.ProductSettingVersionId);


        _context.ProductSettingVersionPositions.RemoveRange(settingPositionsToDelete);

        // Na koniec usuwamy wersje ustawień

        _context.ProductSettingVersions.Remove(productSettingVersionToDelete);
        
        await _context.SaveChangesAsync();

        return;

    }
}
