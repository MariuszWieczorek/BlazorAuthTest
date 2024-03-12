using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.ProductSettingsVersions.Commands.CopyProductSettingVersion;

public class CopyProductSettingVersionCommandHandler : IRequestHandler<CopyProductSettingVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;

    public CopyProductSettingVersionCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUser)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
    }

    public async Task Handle(CopyProductSettingVersionCommand request, CancellationToken cancellationToken)
    {
        await copyProductSettingVersion(request);

        await _context.SaveChangesAsync();

        return;

    }

    private async Task copyProductSettingVersion(CopyProductSettingVersionCommand request)
    {
        var productSettingVersion = await _context.ProductSettingVersions
                    .SingleOrDefaultAsync(x => x.Id == request.ProductSettingVersionId && x.ProductId == request.ProductId);




        var newProductSettingVersion = new ProductSettingVersion
        {
            ProductId = productSettingVersion.ProductId,
            MachineCategoryId = productSettingVersion.MachineCategoryId,
            MachineId = productSettingVersion.MachineId,
            WorkCenterId = productSettingVersion.WorkCenterId,
            Name = productSettingVersion.Name,
            AlternativeNo = productSettingVersion.AlternativeNo,
            ProductSettingVersionNumber = productSettingVersion.ProductSettingVersionNumber + 1,

            Description = productSettingVersion.Description,


            DefaultVersion = false,


            IsAccepted01 = false,
            Accepted01ByUserId = null,
            Accepted01Date = null,

            IsAccepted02 = false,
            Accepted02ByUser = null,
            Accepted02Date = null,

            IsAccepted03 = false,
            Accepted03ByUser = null,
            Accepted03Date = null,

            CreatedByUserId = _currentUser.UserId,
            CreatedDate = _dateTimeService.Now,

            ModifiedByUser = null,
            ModifiedDate = null,

            IsActive = false

        };


        _context.ProductSettingVersions.Add(newProductSettingVersion);

        await copyProductSettingVersionPositions(request, newProductSettingVersion);


    }





    private async Task copyProductSettingVersionPositions(CopyProductSettingVersionCommand request, ProductSettingVersion newProductSettingVersion)
    {
        var positionsToCopy = await _context.ProductSettingVersionPositions
                    .Where(x => x.ProductSettingVersionId == request.ProductSettingVersionId)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var item in positionsToCopy)
        {

            var newPosition = new ProductSettingVersionPosition
            {
                ProductSettingVersion = newProductSettingVersion,
                IsActive = true,
                Description = item.Description,
                MinValue = item.MinValue,
                Value = item.Value,
                MaxValue = item.MaxValue,
                SettingId = item.SettingId,
                Text = item.Text,
            };

            _context.ProductSettingVersionPositions.Add(newPosition);

        }
    }

}
