using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace MwTech.Application.Settings.Queries.GetSettings;

public class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, SettingsViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetSettingsQueryHandler> _logger;

    public GetSettingsQueryHandler(IApplicationDbContext context,
        ILogger<GetSettingsQueryHandler> logger
        )
    {
        _context = context;
        _logger = logger;
    }
    public async Task<SettingsViewModel> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {

        var  settings =  _context.Settings
                    .Include(x => x.SettingCategory)
                    .ThenInclude(x => x.MachineCategory)
                    .Include(x => x.Unit)
                    .AsNoTracking()
                    .AsQueryable();

        var settingsToCount = _context.Settings
            .AsNoTracking()
            .AsQueryable();

        settings = Filter(settings, request.SettingFilter);

        settingsToCount = Filter(settingsToCount, request.SettingFilter);

        var settingsCount = settingsToCount.Count();
        

        settings = settings
                .OrderBy(x => x.SettingCategory.MachineCategory.Name)
                .ThenBy(x => x.SettingCategory.OrdinalNumber)
                .ThenBy(x => x.OrdinalNumber)
                .ThenBy(x => x.SettingNumber);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = settings.Count();
            request.PagingInfo.ItemsPerPage = 200;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                settings = settings
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }


        var settingCategories = await _context.SettingCategories
            .Include(x => x.MachineCategory)
            .OrderBy(x => x.MachineCategory.Name)
            .ThenBy(x => x.OrdinalNumber)
            .ToListAsync();

        var vm = new SettingsViewModel
        {
            SettingFilter = request.SettingFilter,
            SettingCategories = settingCategories,
            Settings = await settings.ToListAsync(),
            PagingInfo = request.PagingInfo,
            MachineCategories = await _context.MachineCategories.OrderBy(x=>x.MachineCategoryNumber).ToListAsync(),
            NumberOfRecords = settingsCount
        };
            
        
        return vm;
    }

    
    private IQueryable<Setting> Filter(IQueryable<Setting> settings, SettingFilter settingFilter)
    {
        if (settingFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(settingFilter.Name))
                settings = settings.Where(x => x.Name.Contains(settingFilter.Name));

            if (!string.IsNullOrWhiteSpace(settingFilter.SettingNumber))
                settings = settings.Where(x => x.SettingNumber.Contains(settingFilter.SettingNumber));

            if (settingFilter.SettingCategoryId != 0)
                settings = settings.Where(x => x.SettingCategoryId == settingFilter.SettingCategoryId);

            if (settingFilter.MachineCategoryId != 0)
                settings = settings.Where(x => x.SettingCategory.MachineCategoryId == settingFilter.MachineCategoryId);

            if (settingFilter.Id != 0)
                settings = settings.Where(x => x.Id == settingFilter.Id);
        }
        return settings;
    }
}
