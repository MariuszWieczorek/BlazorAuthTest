using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.EditProductSettingVersionPosition;

public class EditProductSettingVersionPositionCommandHandler : IRequestHandler<EditProductSettingVersionPositionCommand>
{
    private readonly IApplicationDbContext _context;

    public EditProductSettingVersionPositionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(EditProductSettingVersionPositionCommand request, CancellationToken cancellationToken)
    {
        var productSettingVersionPositionToEdit = _context.ProductSettingVersionPositions
            .SingleOrDefault(p => 
            p.ProductSettingVersionId == request.ProductSettingVersionId 
            && p.Id == request.Id);

        productSettingVersionPositionToEdit.Value = request.Value;
        productSettingVersionPositionToEdit.SettingId = request.SettingId;
        productSettingVersionPositionToEdit.Text = request.Text;
        productSettingVersionPositionToEdit.MinValue = request.MinValue;
        productSettingVersionPositionToEdit.MaxValue = request.MaxValue;
        productSettingVersionPositionToEdit.Description = request.Description;
        
        await _context.SaveChangesAsync();

        return;
    }
}
