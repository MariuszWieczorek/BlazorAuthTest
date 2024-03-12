using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Machines.Commands.AddMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Machines.Queries.GetAddMachineViewModel;

public class GetAddMachineViewModelQueryHandler : IRequestHandler<GetAddMachineViewModelQuery, AddMachineViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddMachineViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddMachineViewModel> Handle(GetAddMachineViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new AddMachineViewModel()
        {
            MachineCategories = await _context.MachineCategories.AsNoTracking().ToListAsync(),
            AddMachineCommand = new AddMachineCommand()
        };

        return vm;
    }
}
