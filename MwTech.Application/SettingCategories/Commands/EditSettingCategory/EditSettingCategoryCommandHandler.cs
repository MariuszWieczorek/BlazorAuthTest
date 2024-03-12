using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.SettingCategories.Commands.EditSettingCategory;

public class EditSettingCategoryCommandHandler : IRequestHandler<EditSettingCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public EditSettingCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditSettingCategoryCommand request, CancellationToken cancellationToken)
    {
        var settingCategory = await _context.SettingCategories.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        settingCategory.SettingCategoryNumber = request.SettingCategoryNumber;
        settingCategory.OrdinalNumber = request.OrdinalNumber;
        settingCategory.Name = request.Name;
        settingCategory.Color = request.Color;
        settingCategory.MachineCategoryId = request.MachineCategoryId;
        settingCategory.Description = request.Description;


        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
