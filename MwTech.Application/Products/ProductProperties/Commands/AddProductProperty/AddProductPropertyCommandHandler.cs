using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductProperties.Commands.AddProductProperty;

public class AddProductPropertyCommandHandler : IRequestHandler<AddProductPropertyCommand>
{
    private readonly IApplicationDbContext _context;

    public AddProductPropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(AddProductPropertyCommand request, CancellationToken cancellationToken)
    {
        var productPropertyToAdd = new ProductProperty
        {
            ProductPropertiesVersionId= request.ProductPropertiesVersionId,
            PropertyId = request.PropertyId,
            Value = request.Value,
            MinValue = request.MinValue,
            MaxValue = request.MaxValue,
            Text = request.Text
        };
        
        _context.ProductProperties.Add(productPropertyToAdd);
        
       
        await _context.SaveChangesAsync();

        return;
    }
}
