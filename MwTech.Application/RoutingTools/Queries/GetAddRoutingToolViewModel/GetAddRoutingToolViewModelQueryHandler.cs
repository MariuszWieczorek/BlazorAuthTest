using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.RoutingTools.Commands.AddRoutingTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.RoutingTools.Queries.GetAddRoutingToolViewModel;

public class GetAddRoutingToolViewModelQueryHandler : IRequestHandler<GetAddRoutingToolViewModelQuery, AddRoutingToolViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddRoutingToolViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddRoutingToolViewModel> Handle(GetAddRoutingToolViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new AddRoutingToolViewModel()
        {
            ProductCategories = await _context.ProductCategories.AsNoTracking().ToListAsync(),
            AddRoutingToolCommand = new AddRoutingToolCommand()
        };

        return vm;
    }
}
