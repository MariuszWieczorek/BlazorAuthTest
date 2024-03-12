using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.SettingCategories.Commands.AddSettingCategory;

public class AddSettingCategoryCommandHandler : IRequestHandler<AddSettingCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public AddSettingCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddSettingCategoryCommand request, CancellationToken cancellationToken)
    {
        var settingCategory = new SettingCategory();

        settingCategory.SettingCategoryNumber = request.SettingCategoryNumber;
        settingCategory.OrdinalNumber = request.OrdinalNumber;
        settingCategory.Name = request.Name;
        settingCategory.Color = request.Color;
        settingCategory.MachineCategoryId = request.MachineCategoryId;
        settingCategory.Description = request.Description;



        await _context.SettingCategories.AddAsync(settingCategory);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
