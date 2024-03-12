using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.RoutingTools.Commands.EditRoutingTool;

namespace MwTech.Application.RoutingTools.Queries.GetEditRoutingToolViewModel;

public class GetEditRoutingToolViewModelQueryHandler : IRequestHandler<GetEditRoutingToolViewModelQuery, EditRoutingToolViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditRoutingToolViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditRoutingToolViewModel> Handle(GetEditRoutingToolViewModelQuery request, CancellationToken cancellationToken)
    {

        var RoutingTool = await _context.RoutingTools.SingleAsync(x => x.Id == request.Id);
        
        var editRoutingToolCommand = new EditRoutingToolCommand
        {
            Id = RoutingTool.Id,
            Name = RoutingTool.Name,
            ToolNumber = RoutingTool.ToolNumber

    };
        

        var vm = new EditRoutingToolViewModel()
        {
            ProductCategories = await _context.ProductCategories.AsNoTracking().ToListAsync(),
            EditRoutingToolCommand = editRoutingToolCommand
        };

        return vm;
    }
}
