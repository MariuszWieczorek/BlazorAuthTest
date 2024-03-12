using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;


namespace MwTech.Application.Units.Commands.AddUnit;

public class AddUnitCommandHandler : IRequestHandler<AddUnitCommand>
{
    private readonly IApplicationDbContext _context;

    public AddUnitCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(AddUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = new MwTech.Domain.Entities.Unit
        {
            Name = request.Name,
            UnitCode = request.UnitCode,
            Description = request.Description,
            Cost = request.Cost,
            Boolean = request.Boolean,
            Weight= request.Weight,
            Time = request.Time,
            Icon = request.Icon    
        };

        await _context.Units.AddAsync(unit);
        await _context.SaveChangesAsync();

        return;

    }
}
