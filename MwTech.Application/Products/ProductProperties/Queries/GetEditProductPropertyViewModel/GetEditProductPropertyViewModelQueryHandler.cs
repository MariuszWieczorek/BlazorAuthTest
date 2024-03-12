using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.ProductProperties.Commands.EditProductProperty;
using MwTech.Application.Props.Queries.GetProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductProperties.Queries.GetEditProductPropertyViewModel;

public class GetEditProductPropertyViewModelQueryHandler : IRequestHandler<GetEditProductPropertyViewModelQuery, EditProductPropertyViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditProductPropertyViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditProductPropertyViewModel> Handle(GetEditProductPropertyViewModelQuery request, CancellationToken cancellationToken)
    {

        var productProperty = _context.ProductProperties
            .Include(x=>x.Property)
            .ThenInclude(x=>x.Unit)
            .SingleOrDefault(x => 
               x.Id == request.ProductPropertyId 
            && x.ProductPropertiesVersionId == request.ProductPropertiesVersionId);


        var productPropertiesVersion = _context.ProductPropertyVersions
            .Include(x => x.Product)
            .SingleOrDefault(x => x.Id == request.ProductPropertiesVersionId);

        var editProductPropertyCommand = new EditProductPropertyCommand
        {
            Id = request.ProductPropertyId,
            PropertyId = productProperty.PropertyId,
            ProductPropertiesVersionId = request.ProductPropertiesVersionId,
            Property = productProperty.Property,
            Value = productProperty.Value,
            MinValue = productProperty.MinValue,
            MaxValue = productProperty.MaxValue,
            Text = productProperty.Text,
            ProductId = productPropertiesVersion.ProductId
        };


        var getPropertiesQueryViewModel = new PropertiesViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x=>x.OrdinalNumber).AsNoTracking().ToListAsync(),
            Properties = await _context.Properties.OrderBy(x => x.PropertyNumber).ToListAsync(),
            PropertyFilter = new PropertyFilter()
        };

        var vm = new EditProductPropertyViewModel
        {
            EditProductPropertyCommand = editProductPropertyCommand,
            ProductPropertiesVersion = productPropertiesVersion,
            Properties = await _context.Properties.OrderBy(x => x.PropertyNumber).ToListAsync(),
            GetPropertiesQueryViewModel = getPropertiesQueryViewModel
            
        };

        return vm;

    }
}
