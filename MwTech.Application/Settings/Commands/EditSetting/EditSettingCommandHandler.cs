using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Settings.Commands.EditSetting;

public class EditSettingCommandHandler : IRequestHandler<EditSettingCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditSettingCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == request.Id);

        setting.Name = request.Name;
        setting.SettingNumber = request.SettingNumber;
        setting.OrdinalNumber = request.OrdinalNumber;
        setting.Description = request.Description;
        setting.MachineCategoryId = request.MachineCategoryId;
        setting.SettingCategoryId = request.SettingCategoryId;
        setting.Text = request.Text;
        setting.MinValue = request.MinValue;
        setting.Value = request.Value;
        setting.MaxValue = request.MaxValue;
        setting.IsActive = request.IsActive;
        setting.IsEditable = request.IsEditable;
        setting.AlwaysOnPrintout = request.AlwaysOnPrintout;
        setting.HideOnPrintout = request.HideOnPrintout;
        setting.IsNumeric = request.IsNumeric;
        setting.UnitId = request.UnitId;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
