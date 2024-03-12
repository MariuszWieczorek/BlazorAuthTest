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

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.CopyProductSettingVersionPositions;

public class CopyProductSettingVersionPositionsCommandHandler : IRequestHandler<CopyProductSettingVersionPositionsCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public CopyProductSettingVersionPositionsCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(CopyProductSettingVersionPositionsCommand request, CancellationToken cancellationToken)
    {
        var product = _context.Products.SingleOrDefault(x => x.Id == request.ProductId);

        if (product != null)
        {

            // wersja ustawień do modyfikacji
            var currentProductSettingVersion = _context.ProductSettingVersions
                .SingleOrDefault(x => x.Id == request.ProductSettingVersionId);

            // aktualnie zapamiętane ustawienia w wybranej wersji
            var currentProductSettingVersionPositions = await _context.ProductSettingVersionPositions
                .Where(x => x.ProductSettingVersionId == request.ProductSettingVersionId)
                .ToListAsync();


            
            // pozycje do skopiowanie
            var settingsToCopy = await _context.ProductSettingVersionPositions
                .Include(x=>x.Setting)
                .Where(x => x.ProductSettingVersionId == request.SourceProductSettingVersionId)
                .AsNoTracking()
                .ToListAsync();


            foreach (var item in settingsToCopy)
            {

                var existedSetting = currentProductSettingVersionPositions
                    .FirstOrDefault(x => x.SettingId == item.SettingId);

                if ( existedSetting == null)
                {
                    var productSettingVersionPositionToAdd = new ProductSettingVersionPosition
                    {
                        ProductSettingVersionId = request.ProductSettingVersionId,
                        SettingId = item.SettingId,
                        MinValue = item.MinValue,
                        Value = item.Value,
                        MaxValue= item.MaxValue,
                        Text = item.Text,
                        Description = item.Description,
                        IsActive= item.IsActive
                    };
                    await _context.ProductSettingVersionPositions.AddAsync(productSettingVersionPositionToAdd);
                }
                else
                {
                    existedSetting.MinValue = item.MinValue;
                    existedSetting.Value = item.Value;
                    existedSetting.MaxValue = item.MaxValue;
                    existedSetting.Text = item.Text;
                    existedSetting.Description = item.Description;
                    existedSetting.IsActive = item.IsActive;
                        
                }

            }
            await _context.SaveChangesAsync();
        }

        return;
    }
}
