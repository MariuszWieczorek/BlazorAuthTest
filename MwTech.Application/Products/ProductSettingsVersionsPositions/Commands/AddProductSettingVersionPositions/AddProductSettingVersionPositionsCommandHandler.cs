using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.AddProductSettingVersionPositions;

public class AddProductSettingVersionPositionsCommandHandler : IRequestHandler<AddProductSettingVersionPositionsCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddProductSettingVersionPositionsCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(AddProductSettingVersionPositionsCommand request, CancellationToken cancellationToken)
    {
        var product = _context.Products.SingleOrDefault(x => x.Id == request.ProductId);

        if (product != null)
        {

            var productSettingVersionPositions = await _context.ProductSettingVersionPositions
                .Where(x => x.ProductSettingVersionId == request.ProductSettingVersionId)
                .ToListAsync();


            var productSettingVersion = _context.ProductSettingVersions
                .SingleOrDefault(x => x.Id == request.ProductSettingVersionId);


            var settingsToAdd = await _context.Settings
                .Include(x=>x.SettingCategory)
                .Where(x => x.SettingCategory.MachineCategoryId == productSettingVersion.MachineCategoryId)
                .ToListAsync();


            foreach (var item in settingsToAdd)
            {
                if (!productSettingVersionPositions.Where(x => x.SettingId == item.Id).Any())
                {
                    var productSettingVersionPositionToAdd = new ProductSettingVersionPosition
                    {
                        ProductSettingVersionId = request.ProductSettingVersionId,
                        SettingId = item.Id,
                        MinValue = item.MinValue,
                        Value = item.MaxValue,
                        MaxValue= item.MaxValue,
                        Text = item.Text,
                        Description = item.Description,
                        IsActive= item.IsActive
                    };
                    await _context.ProductSettingVersionPositions.AddAsync(productSettingVersionPositionToAdd);
                }
            }
            await _context.SaveChangesAsync();
        }

        return;
    }
}
