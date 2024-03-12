using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.RoutingTools.Queries.GetRoutingTools;

public class GetRoutingToolsQueryHandler : IRequestHandler<GetRoutingToolsQuery, GetRoutingToolsViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetRoutingToolsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<GetRoutingToolsViewModel> Handle(GetRoutingToolsQuery request, CancellationToken cancellationToken)
    {
        var RoutingTools = _context.RoutingTools
            .AsNoTracking()
            .AsQueryable();

        RoutingTools = Filter(RoutingTools, request.RoutingToolFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = RoutingTools.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                RoutingTools = RoutingTools
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var RoutingToolsList = await RoutingTools.ToListAsync();

        var vm = new GetRoutingToolsViewModel
            { 
              RoutingTools = RoutingToolsList,
              RoutingToolFilter = request.RoutingToolFilter,
              PagingInfo = request.PagingInfo,
              ProductCategories = await _context.ProductCategories.ToListAsync()
            };

        return vm;
           
    }

    public IQueryable<RoutingTool> Filter(IQueryable<RoutingTool> RoutingTools, RoutingToolFilter RoutingToolFilter)
    {
        if (RoutingToolFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(RoutingToolFilter.Name))
                RoutingTools = RoutingTools.Where(x => x.Name.Contains(RoutingToolFilter.Name));

            if (!string.IsNullOrWhiteSpace(RoutingToolFilter.ToolNumber))
                RoutingTools = RoutingTools.Where(x => x.ToolNumber.Contains(RoutingToolFilter.ToolNumber));

            //if (RoutingToolFilter.ProductCategoryId != 0)
              //  RoutingTools = RoutingTools.Where(x => x.ProductCategoryId == RoutingToolFilter.ProductCategoryId);
        }

        return RoutingTools;
    }
}
