using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.MachineCategories.Queries.GetMachineCategories;

public class GetMachineCategoriesQueryHandler : IRequestHandler<GetMachineCategoriesQuery, MachineCategoriesViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetMachineCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<MachineCategoriesViewModel> Handle(GetMachineCategoriesQuery request, CancellationToken cancellationToken)
    {
        var machineCategories = _context.MachineCategories
            .Include(x=>x.ProductCategory)
            .OrderBy(x=>x.MachineCategoryNumber)
            .AsNoTracking()
            .AsQueryable();

        machineCategories = Filter(machineCategories, request.MachineCategoryFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = machineCategories.Count();
            request.PagingInfo.ItemsPerPage = 10;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                machineCategories = machineCategories
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var machineCategoriesList = await machineCategories.ToListAsync();

        var vm = new MachineCategoriesViewModel
            { MachineCategories = machineCategoriesList,
              MachineCategoryFilter = request.MachineCategoryFilter,
              PagingInfo = request.PagingInfo
            };

        return vm;
           
    }

    public IQueryable<MachineCategory> Filter(IQueryable<MachineCategory> machineCategories, MachineCategoryFilter machineCategoryFilter)
    {
        if (machineCategoryFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(machineCategoryFilter.Name))
                machineCategories = machineCategories.Where(x => x.Name.Contains(machineCategoryFilter.Name));

            if (!string.IsNullOrWhiteSpace(machineCategoryFilter.MachineCategoryNumber))
                machineCategories = machineCategories.Where(x => x.MachineCategoryNumber.Contains(machineCategoryFilter.MachineCategoryNumber));
        }
        
        return machineCategories;
    }
}
