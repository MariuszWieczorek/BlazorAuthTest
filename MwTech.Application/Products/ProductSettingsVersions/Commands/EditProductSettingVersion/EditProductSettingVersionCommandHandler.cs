using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.EditProductSettingVersion;

public class EditProductSettingVersionCommandHandler : IRequestHandler<EditProductSettingVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditProductSettingVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditProductSettingVersionCommand request, CancellationToken cancellationToken)
    {
        var productSettingVersion = await _context.ProductSettingVersions
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.ProductId == request.ProductId);

        productSettingVersion.ProductSettingVersionNumber = request.ProductSettingVersionNumber;
        productSettingVersion.AlternativeNo = request.AlternativeNo;
        productSettingVersion.Name = request.Name;    
        productSettingVersion.Description = request.Description;

        productSettingVersion.MachineCategoryId = request.MachineCategoryId;
        productSettingVersion.MachineId = request.MachineId;
        productSettingVersion.WorkCenterId = request.WorkCenterId;
        
        productSettingVersion.ProductId = request.ProductId;

        productSettingVersion.ModifiedByUserId = _currentUserService.UserId;
        productSettingVersion.ModifiedDate = _dateTimeService.Now;

        productSettingVersion.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
