using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Machines.Queries.GetMachines;

public class GetMachinesQueryHandler : IRequestHandler<GetMachinesQuery, GetMachinesViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetMachinesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<GetMachinesViewModel> Handle(GetMachinesQuery request, CancellationToken cancellationToken)
    {
        var machines = _context.Machines
            .Include(x=>x.MachineCategory)
            .AsNoTracking()
            .AsQueryable();

        machines = Filter(machines, request.MachineFilter);

        var MachinesList = await machines.OrderBy(x=>x.MachineNumber).ToListAsync();

        var vm = new GetMachinesViewModel
            { 
              Machines = MachinesList,
              MachineFilter = request.MachineFilter,
              MachineCategories = await _context.MachineCategories.ToListAsync()
            };

        return vm;
           
    }

    public IQueryable<Machine> Filter(IQueryable<Machine> operations, MachineFilter machineFilter)
    {
        if (machineFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(machineFilter.Name))
                operations = operations.Where(x => x.Name.Contains(machineFilter.Name));

            if (!string.IsNullOrWhiteSpace(machineFilter.MachineNumber))
                operations = operations.Where(x => x.MachineNumber.Contains(machineFilter.MachineNumber));

            if (machineFilter.MachineCategoryId != 0)
                operations = operations.Where(x => x.MachineCategoryId == machineFilter.MachineCategoryId);
        }

        return operations;
    }
}
