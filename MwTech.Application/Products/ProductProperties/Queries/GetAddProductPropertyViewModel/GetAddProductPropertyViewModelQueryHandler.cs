using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.ProductProperties.Commands.AddProductProperty;
using MwTech.Application.Props.Queries.GetProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductProperties.Queries.GetAddProductPropertyViewModel;

public class GetAddProductPropertyViewModelQueryHandler : IRequestHandler<GetAddProductPropertyViewModelQuery, AddProductPropertyViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddProductPropertyViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddProductPropertyViewModel> Handle(GetAddProductPropertyViewModelQuery request, CancellationToken cancellationToken)
    {

     
        var productPropertiesVersion = _context.ProductPropertyVersions
            .Include(x => x.Product)
            .SingleOrDefault(x => x.Id == request.ProductPropertiesVersionId);

        var getPropertiesQueryViewModel = new PropertiesViewModel
        {
            ProductCategories = await _context.ProductCategories.OrderBy(x=>x.OrdinalNumber).AsNoTracking().ToListAsync(),
            Properties = await _context.Properties.OrderBy(x => x.PropertyNumber).ToListAsync(),
            PropertyFilter = new PropertyFilter()
        };



        var addProductPropertyCommand = new AddProductPropertyCommand
        {
            ProductPropertiesVersionId = request.ProductPropertiesVersionId,
            PropertyId = 0,
            Property = null,
            Value = 0,
            MinValue = 0,
            MaxValue = 0,
            Text = "",
            ProductId = productPropertiesVersion.ProductId
        };



        var vm = new AddProductPropertyViewModel
        {
            AddProductPropertyCommand = addProductPropertyCommand,
            ProductPropertiesVersion= productPropertiesVersion,
            Properties = await _context.Properties.OrderBy(x => x.PropertyNumber).ToListAsync(),
            GetPropertiesQueryViewModel = getPropertiesQueryViewModel,
            
        };

        return vm;

    }
}
