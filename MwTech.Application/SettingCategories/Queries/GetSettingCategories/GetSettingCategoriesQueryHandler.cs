using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.SettingCategories.Queries.GetSettingCategories;

public class GetSettingCategoriesQueryHandler : IRequestHandler<GetSettingCategoriesQuery, SettingCategoriesViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetSettingCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SettingCategoriesViewModel> Handle(GetSettingCategoriesQuery request, CancellationToken cancellationToken)
    {
        var settingCategories = _context.SettingCategories
                        .Include(x => x.MachineCategory)
                        .AsNoTracking()
                        .AsQueryable();

        settingCategories = Filter(settingCategories, request.SettingCategoryFilter);

        settingCategories = settingCategories
                    .OrderBy(x => x.MachineCategoryId)
                    .ThenBy(x => x.OrdinalNumber)
                    .ThenBy(x => x.SettingCategoryNumber);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = settingCategories.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                settingCategories = settingCategories
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var SettingCategoriesList = await settingCategories.ToListAsync();

        var vm = new SettingCategoriesViewModel
            { 
              SettingCategories = SettingCategoriesList,
              SettingCategoryFilter = request.SettingCategoryFilter,
              PagingInfo = request.PagingInfo,
              MachineCategories = await _context.MachineCategories.ToListAsync()   
            };

        return vm;
           
    }

    public IQueryable<SettingCategory> Filter(IQueryable<SettingCategory> settingCategories, SettingCategoryFilter settingCategoryFilter)
    {
        if (settingCategoryFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(settingCategoryFilter.Name))
                settingCategories = settingCategories.Where(x => x.Name.Contains(settingCategoryFilter.Name));

            if (!string.IsNullOrWhiteSpace(settingCategoryFilter.SettingCategoryNumber))
                settingCategories = settingCategories.Where(x => x.SettingCategoryNumber.Contains(settingCategoryFilter.SettingCategoryNumber));

            if (settingCategoryFilter.MachineCategoryId != 0)
                settingCategories = settingCategories.Where(x => x.MachineCategoryId == settingCategoryFilter.MachineCategoryId);

            if (settingCategoryFilter.Id != 0)
                settingCategories = settingCategories.Where(x => x.Id == settingCategoryFilter.Id);
        }

        return settingCategories;
    }
}
