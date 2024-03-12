using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.DeleteProductSettingVersionPosition;

public class DeleteProductSettingVersionPositionCommandHandler : IRequestHandler<DeleteProductSettingVersionPositionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductSettingVersionPositionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteProductSettingVersionPositionCommand request, CancellationToken cancellationToken)
    {
        var productSettingVersionPositionToDelete = _context.ProductSettingVersionPositions
            .SingleOrDefault(p => 
            p.ProductSettingVersionId == request.ProductSettingVersionId
            && p.Id == request.Id);

        _context.ProductSettingVersionPositions.Remove(productSettingVersionPositionToDelete);
        
        await _context.SaveChangesAsync();

        return;
    }
}
