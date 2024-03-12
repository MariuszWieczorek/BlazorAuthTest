using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Props.Commands.AddProperty;

public class AddPropertyCommandHandler : IRequestHandler<AddPropertyCommand>
{
    private readonly IApplicationDbContext _context;

    public AddPropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddPropertyCommand request, CancellationToken cancellationToken)
    {
        var property = new Property();
        
        property.PropertyNumber = request.PropertyNumber;

        if (!String.IsNullOrWhiteSpace(request.ScadaPropertyNumber))
        {
            property.ScadaPropertyNumber = request.ScadaPropertyNumber;
        }
        else
        {
            property.ScadaPropertyNumber = request.PropertyNumber;
        }

        property.Name = request.Name;
        property.Description = request.Description;
        property.ProductCategoryId = request.ProductCategoryId;
        property.UnitId = request.UnitId;
        property.IsGeneralProperty = request.IsGeneralProperty;
        property.IsVersionProperty = request.IsVersionProperty;
        property.OrdinalNo = request.OrdinalNo;
        property.DecimalPlaces = request.DecimalPlaces;
        property.HideOnReport = request.HideOnReport;

        await _context.Properties.AddAsync(property);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
