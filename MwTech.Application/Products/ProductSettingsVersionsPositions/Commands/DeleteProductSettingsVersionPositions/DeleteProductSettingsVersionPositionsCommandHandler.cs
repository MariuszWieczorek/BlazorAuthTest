using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.DeleteProductSettingsVersionPositions;

public class DeleteProductSettingsVersionPositionsCommandHandler : IRequestHandler<DeleteProductSettingsVersionPositionsCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductSettingsVersionPositionsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteProductSettingsVersionPositionsCommand request, CancellationToken cancellationToken)
    {
        var productSettingVersionPositionToDelete = _context.ProductSettingVersionPositions
            .Include(x=>x.ProductSettingVersion)
            .Where(p => 
            p.ProductSettingVersionId == request.ProductSettingVersionId
            && p.ProductSettingVersion.ProductId == request.ProductId);

        _context.ProductSettingVersionPositions.RemoveRange(productSettingVersionPositionToDelete);
        
        await _context.SaveChangesAsync();

        return;
    }
}
