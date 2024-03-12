using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.EditProductSettingVersionPosition;
using MwTech.Application.Settings.Queries.GetSettings;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Queries.GetEditProductSettingVersionPositionViewModel;

public class GetEditProductSettingVersionPositionViewModelQueryHandler : IRequestHandler<GetEditProductSettingVersionPositionViewModelQuery, EditProductSettingVersionPositionViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditProductSettingVersionPositionViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditProductSettingVersionPositionViewModel> Handle(GetEditProductSettingVersionPositionViewModelQuery request, CancellationToken cancellationToken)
    {

        var productSettingVersionosition = _context.ProductSettingVersionPositions
            .Include(x=>x.Setting)
            .ThenInclude(x=>x.Unit)
            .SingleOrDefault(x => 
                 x.Id == request.ProductSettingId
              && x.ProductSettingVersionId == request.ProductSettingVersionId);


        var editProductSettingVersionPositionCommand = new EditProductSettingVersionPositionCommand
        {
            Id = request.ProductSettingId,
            ProductSettingVersionId = request.ProductSettingVersionId,
            ProductId = request.ProductId,
            SettingId = productSettingVersionosition.SettingId,
            Setting = productSettingVersionosition.Setting,
            MinValue = productSettingVersionosition.MinValue, 
            Value = productSettingVersionosition.Value,
            MaxValue = productSettingVersionosition.MaxValue,  
            Text = productSettingVersionosition.Text,
            Description= productSettingVersionosition.Description,
        };


        var settingsViewModel = new SettingsViewModel
        {
            SettingCategories = await _context.SettingCategories.OrderBy(x=>x.OrdinalNumber).AsNoTracking().ToListAsync(),
            Settings = await _context.Settings.OrderBy(x => x.SettingNumber).ToListAsync(),
            SettingFilter = new SettingFilter(),
            MachineCategories = await _context.MachineCategories.OrderBy(x=>x.MachineCategoryNumber).ToListAsync(),
        };

        var vm = new EditProductSettingVersionPositionViewModel
        {
            EditProductSettingVersionPositionCommand = editProductSettingVersionPositionCommand,
            SettingsViewModel = settingsViewModel
        };

        return vm;

    }
}
