using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Units.Commands.EditUnit;

public class EditUnitCommandHandler : IRequestHandler<EditUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public EditUnitCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(EditUnitCommand request, CancellationToken cancellationToken)
    {
        var unitToUpdate = _context.Units.Single(x => x.Id == request.Id);
        
        unitToUpdate.Name = request.Name;
        unitToUpdate.UnitCode = request.UnitCode;
        unitToUpdate.Description = request.Description;
        unitToUpdate.Weight = request.Weight;
        unitToUpdate.Time = request.Time;
        unitToUpdate.Cost = request.Cost;
        unitToUpdate.Boolean = request.Boolean;
        unitToUpdate.PeriodInSeconds = request.PeriodInSeconds;
        unitToUpdate.Icon = request.Icon;


        _context.Units.Update(unitToUpdate);

        await _context.SaveChangesAsync();

        return;
    }
}
