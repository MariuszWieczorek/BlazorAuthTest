using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Props.Commands.AddProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Props.Queries.GetAddPropertyViewModel;

public class GetAddPropertyViewModelQueryHandler : IRequestHandler<GetAddPropertyViewModelQuery, AddPropertyViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddPropertyViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddPropertyViewModel> Handle(GetAddPropertyViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new AddPropertyViewModel()
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x=>x.OrdinalNumber).AsNoTracking().ToListAsync(),
            Units = await _context.Units.AsNoTracking().ToListAsync(),
            AddPropertyCommand = new AddPropertyCommand
            {
                OrdinalNo = 1,
                DecimalPlaces = 0,
            }
        };

        return vm;
    }
}
