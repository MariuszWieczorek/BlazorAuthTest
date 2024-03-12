using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.AddProductSettingVersionPosition;

public class AddProductSettingVersionPositionCommandHandler : IRequestHandler<AddProductSettingVersionPositionCommand>
{
    private readonly IApplicationDbContext _context;

    public AddProductSettingVersionPositionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(AddProductSettingVersionPositionCommand request, CancellationToken cancellationToken)
    {
        var productVersionPropertyToAdd = new ProductSettingVersionPosition
        {
            ProductSettingVersionId = request.ProductSettingVersionId,
            SettingId = request.SettingId,
            Text = request.Text,
            MinValue = request.MinValue,
            Value = request.Value,
            MaxValue = request.MaxValue,
            Description= request.Description,
        };
        
        _context.ProductSettingVersionPositions.Add(productVersionPropertyToAdd);
        
       
        await _context.SaveChangesAsync();

        return;
    }
}
