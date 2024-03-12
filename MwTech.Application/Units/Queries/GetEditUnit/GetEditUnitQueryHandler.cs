using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Units.Commands.EditUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Units.Queries.GetUnits.GetEditUnit;

public class GetEditUnitQueryHandler : IRequestHandler<GetEditUnitQuery, EditUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public GetEditUnitQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditUnitCommand> Handle(GetEditUnitQuery request, CancellationToken cancellationToken)
    {
        
        var unitToEdit =  _context.Units.Single(x => x.Id == request.Id);

        var vm = new EditUnitCommand
        {
            Id = request.Id,
            UnitCode = unitToEdit.UnitCode,
            Name = unitToEdit.Name,
            Description = unitToEdit.Description,
            Weight = unitToEdit.Weight,
            Cost = unitToEdit.Cost,
            Time = unitToEdit.Time,
            Boolean = unitToEdit.Boolean,
            PeriodInSeconds = unitToEdit.PeriodInSeconds,
            Icon = unitToEdit.Icon
        };

        return vm;
    }
}
