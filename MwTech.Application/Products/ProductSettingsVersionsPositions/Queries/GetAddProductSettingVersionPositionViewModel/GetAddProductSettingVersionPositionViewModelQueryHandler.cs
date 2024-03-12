using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.AddProductSettingVersionPosition;
using MwTech.Application.Settings.Queries.GetSettings;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Queries.GetAddProductSettingVersionPositionViewModel;

public class GetAddProductSettingVersionPositionViewModelQueryHandler : IRequestHandler<GetAddProductSettingVersionPositionViewModelQuery, AddProductSettingVersionPositionViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddProductSettingVersionPositionViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddProductSettingVersionPositionViewModel> Handle(GetAddProductSettingVersionPositionViewModelQuery request, CancellationToken cancellationToken)
    {

     
        var addProductSettingVersionPositionCommand = new AddProductSettingVersionPositionCommand
        {
            ProductSettingVersionId = request.ProductSettingVersionId,
            ProductId = request.ProductId,
            SettingId = 0,
            Setting = null,
            Value = 0,
            Text = ""
        };


        var settingsViewModel = new SettingsViewModel
        {
            SettingCategories = await _context.SettingCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            Settings = await _context.Settings.OrderBy(x => x.SettingNumber).ToListAsync(),
            SettingFilter = new SettingFilter(),
            MachineCategories = await _context.MachineCategories.OrderBy(x=>x.MachineCategoryNumber).ToListAsync(),
        };

        var vm = new AddProductSettingVersionPositionViewModel
        {
            AddProductSettingVersionPositionCommand = addProductSettingVersionPositionCommand,
            SettingsViewModel = settingsViewModel
            
        };

        return vm;

    }
}
