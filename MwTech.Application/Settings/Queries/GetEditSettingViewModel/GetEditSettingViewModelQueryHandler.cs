using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Settings.Commands.EditSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Settings.Queries.GetEditSettingViewModel;

public class GetEditSettingViewModelQueryHandler : IRequestHandler<GetEditSettingViewModelQuery, EditSettingViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditSettingViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditSettingViewModel> Handle(GetEditSettingViewModelQuery request, CancellationToken cancellationToken)
    {

        var setting = _context.Settings.SingleOrDefault(x => x.Id == request.SettingId);

        var settingCategories = await _context.SettingCategories
            .OrderBy(x => x.SettingCategoryNumber)
            .AsNoTracking()
            .ToListAsync();

        var machineCategories = await _context.MachineCategories
            .OrderBy(x => x.Name)
            .AsNoTracking()
            .ToListAsync();

        var units = await _context.Units
            .OrderBy(x => x.UnitCode)
            .AsNoTracking()
            .ToListAsync();


        var editSettingCommand = new EditSettingCommand
        {
            Id = request.SettingId,
            SettingNumber = setting.SettingNumber,
            Name = setting.Name,
            OrdinalNumber = setting.OrdinalNumber,
            Description = setting.Description,
            MachineCategoryId = setting.MachineCategoryId,
            SettingCategoryId = setting.SettingCategoryId,
            Text = setting.Text,
            MinValue = setting.MinValue,
            Value = setting.Value,
            MaxValue = setting.MaxValue,
            IsEditable = setting.IsEditable,
            IsActive = setting.IsActive,
            IsNumeric = setting.IsNumeric,
            AlwaysOnPrintout = setting.AlwaysOnPrintout,
            HideOnPrintout = setting.HideOnPrintout,
            UnitId = setting.UnitId
        };


        var vm = new EditSettingViewModel
        {
            EditSettingCommand = editSettingCommand,
            SettingCategories = settingCategories,
            Units = units,
            MachineCategories = machineCategories
        };
        return vm;
    }
}
