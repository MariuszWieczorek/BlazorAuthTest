using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Props.Commands.EditProperty;

public class EditPropertyCommandHandler : IRequestHandler<EditPropertyCommand>
{
    private readonly IApplicationDbContext _context;

    public EditPropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditPropertyCommand request, CancellationToken cancellationToken)
    {
        var property = await _context.Properties.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        property.PropertyNumber = request.PropertyNumber;
        property.ScadaPropertyNumber = request.ScadaPropertyNumber;
        property.Name = request.Name;
        property.Description = request.Description;
        property.ProductCategoryId = request.ProductCategoryId;
        property.UnitId = request.UnitId;
        property.IsGeneralProperty = request.IsGeneralProperty;
        property.IsVersionProperty = request.IsVersionProperty;
        property.OrdinalNo = request.OrdinalNo;
        property.DecimalPlaces = request.DecimalPlaces;
        property.HideOnReport = request.HideOnReport;

                
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
