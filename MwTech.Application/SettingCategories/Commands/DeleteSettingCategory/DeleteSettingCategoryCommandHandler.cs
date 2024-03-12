using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.SettingCategories.Commands.DeleteSettingCategory;

public class DeleteSettingCategoryCommandHandler : IRequestHandler<DeleteSettingCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSettingCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteSettingCategoryCommand request, CancellationToken cancellationToken)
    {
        
        var settingCategoryToDelete = await _context.SettingCategories.SingleOrDefaultAsync(x => x.Id == request.Id);
        _context.SettingCategories.Remove(settingCategoryToDelete);
        await _context.SaveChangesAsync();

        return;

    }
}
