using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Settings.Commands.AddSetting;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Settings.Commands.EditSetting;

public class AddSettingCommandHandler : IRequestHandler<AddSettingCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddSettingCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddSettingCommand request, CancellationToken cancellationToken)
    {

        var setting = new Setting
        {
            Name = request.Name,
            SettingNumber = request.SettingNumber,
            OrdinalNumber = request.OrdinalNumber,
            Description = request.Description,
            MachineCategoryId = request.MachineCategoryId,
            SettingCategoryId = request.SettingCategoryId,
            Text = request.Text,
            MinValue = request.MinValue,
            Value = request.Value,
            MaxValue = request.MaxValue,
            IsActive = request.IsActive,
            IsEditable = request.IsEditable,
            AlwaysOnPrintout = request.AlwaysOnPrintout,
            HideOnPrintout = request.HideOnPrintout,
            IsNumeric = request.IsNumeric,
            UnitId = request.UnitId,
        };



        _context.Settings.Add(setting); 
        
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
