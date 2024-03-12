using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductProperties.Commands.EditProductProperty;

public class EditProductPropertyCommandHandler : IRequestHandler<EditProductPropertyCommand>
{
    private readonly IApplicationDbContext _context;

    public EditProductPropertyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(EditProductPropertyCommand request, CancellationToken cancellationToken)
    {
        var productPropertyToEdit = _context.ProductProperties
            .SingleOrDefault(p => p.ProductPropertiesVersionId == request.ProductPropertiesVersionId && p.Id == request.Id);

        productPropertyToEdit.Value = request.Value;
        productPropertyToEdit.MinValue = request.MinValue;
        productPropertyToEdit.MaxValue = request.MaxValue;
        productPropertyToEdit.PropertyId = request.PropertyId;
        productPropertyToEdit.Text = request.Text;
        
        await _context.SaveChangesAsync();

        return;
    }
}
