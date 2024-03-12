using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Operations.Commands.AddOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Operations.Queries.GetAddOperationViewModel;

public class GetAddOperationViewModelQueryHandler : IRequestHandler<GetAddOperationViewModelQuery, AddOperationViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddOperationViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddOperationViewModel> Handle(GetAddOperationViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new AddOperationViewModel()
        {
            ProductCategories = await _context.ProductCategories.AsNoTracking().ToListAsync(),
            Units = await _context.Units.AsNoTracking().ToListAsync(),
            AddOperationCommand = new AddOperationCommand()
        };

        return vm;
    }
}
