using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Currencies.Queries.GetCurrencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MwTech.Domain.Entities;
using Unit = MwTech.Domain.Entities.Unit;
using Microsoft.EntityFrameworkCore;

namespace MwTech.Application.Units.Queries.GetUnits;

public class GetUnitsQueryHandler : IRequestHandler<GetUnitsQuery, UnitsViewModel> 
{
    private readonly IApplicationDbContext _context;

    public GetUnitsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UnitsViewModel> Handle(GetUnitsQuery request, CancellationToken cancellationToken)
    {
        var units = _context.Units
            .AsNoTracking()
            .AsQueryable();
        
        units = Filter(units, request.UnitFilter);

        var unitsList = await units.ToListAsync();

        var vm = new UnitsViewModel
        {
            Units = unitsList,
            UnitFilter = request.UnitFilter
        };

        return vm;

    }

    public IQueryable<Unit> Filter(IQueryable<Unit> units, UnitFilter unitFilter)
    {
        if (unitFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(unitFilter.Name))
                units = units.Where(x => x.Name.Contains(unitFilter.Name));

            if (!string.IsNullOrWhiteSpace(unitFilter.UnitCode))
                units = units.Where(x => x.UnitCode.Contains(unitFilter.UnitCode));

        }

        return units;
    }
}
