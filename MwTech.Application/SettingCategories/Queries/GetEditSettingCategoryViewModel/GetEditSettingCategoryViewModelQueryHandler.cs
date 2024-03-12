using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.SettingCategories.Commands.EditSettingCategory;

namespace MwTech.Application.SettingCategories.Queries.GetEditSettingCategoryViewModel;

public class GetEditSettingCategoryViewModelQueryHandler : IRequestHandler<GetEditSettingCategoryViewModelQuery, EditSettingCategoryViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditSettingCategoryViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditSettingCategoryViewModel> Handle(GetEditSettingCategoryViewModelQuery request, CancellationToken cancellationToken)
    {

        var settingCategory = await _context.SettingCategories.SingleAsync(x => x.Id == request.Id);
        
        var editSettingCategoryCommand = new EditSettingCategoryCommand
        {
            Id = settingCategory.Id,
            OrdinalNumber = settingCategory.OrdinalNumber,
            Name = settingCategory.Name,
            SettingCategoryNumber = settingCategory.SettingCategoryNumber,
            Color = settingCategory.Color,
            Description = settingCategory.Description,
            MachineCategoryId = settingCategory.MachineCategoryId
        };
        

        var vm = new EditSettingCategoryViewModel()
        {
            MachineCategories = await _context.MachineCategories.AsNoTracking().ToListAsync(),
            EditSettingCategoryCommand = editSettingCategoryCommand
        };

        return vm;
    }
}
