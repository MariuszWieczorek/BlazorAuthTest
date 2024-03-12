using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Settings.Commands.AddSetting;
using MwTech.Application.Settings.Commands.EditSetting;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Settings.Queries.GetAddSettingViewModel;

public class GetAddSettingViewModelQueryHandler : IRequestHandler<GetAddSettingViewModelQuery, AddSettingViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddSettingViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddSettingViewModel> Handle(GetAddSettingViewModelQuery request, CancellationToken cancellationToken)
    {

        var setting = new Setting
        {

        };

        var settingCategories = await _context.SettingCategories
            .OrderBy(x => x.SettingCategoryNumber)
            .AsNoTracking()
            .ToListAsync();

        var units = await _context.Units
            .OrderBy(x => x.UnitCode)
            .AsNoTracking()
            .ToListAsync();

        var machineCategories = await _context.MachineCategories
            .OrderBy(x => x.Name)
            .AsNoTracking()
            .ToListAsync();

        var addSettingCommand = new AddSettingCommand
        {
            SettingNumber = String.Empty,
            Name = String.Empty,
            OrdinalNumber = 0,
            Description = null,
            MachineCategoryId = 0,
            SettingCategoryId = 0,
            Text = null,
            MinValue = 0,
            Value = 0,
            MaxValue = 0,
            IsEditable = true,
            IsActive = true,
            IsNumeric = true,
            AlwaysOnPrintout = false,
            HideOnPrintout = false,
            UnitId = 0,
        };


        var vm = new AddSettingViewModel
        {
            AddSettingCommand = addSettingCommand,
            SettingCategories = settingCategories,
            Units = units,
            MachineCategories = machineCategories
        };

        return vm;
    }
}
