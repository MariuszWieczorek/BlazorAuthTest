using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Props.Commands.EditProperty;

namespace MwTech.Application.Props.Queries.GetEditPropertyViewModel;

public class GetEditPropertyViewModelQueryHandler : IRequestHandler<GetEditPropertyViewModelQuery, EditPropertyViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditPropertyViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditPropertyViewModel> Handle(GetEditPropertyViewModelQuery request, CancellationToken cancellationToken)
    {

        var property = await _context.Properties.SingleAsync(x => x.Id == request.Id);
        
        var editPropertyCommand = new EditPropertyCommand
        {
            Id = property.Id,
            Name = property.Name,
            PropertyNumber = property.PropertyNumber,
            ScadaPropertyNumber = property.ScadaPropertyNumber,
            Description = property.Description,
            IsGeneralProperty = property.IsGeneralProperty,
            IsVersionProperty = property.IsVersionProperty,
            UnitId = property.UnitId,
            ProductCategoryId = property.ProductCategoryId,
            OrdinalNo = property.OrdinalNo,
            DecimalPlaces = property.DecimalPlaces,
            HideOnReport = property.HideOnReport,
        };
        

        var vm = new EditPropertyViewModel()
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x=>x.OrdinalNumber).AsNoTracking().ToListAsync(),
            Units = await _context.Units.AsNoTracking().ToListAsync(),
            EditPropertyCommand = editPropertyCommand
        };

        return vm;
    }
}
