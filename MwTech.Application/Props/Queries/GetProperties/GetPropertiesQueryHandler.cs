using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Props.Queries.GetProperties;

public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, PropertiesViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetPropertiesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PropertiesViewModel> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
    {
        var properties = _context.Properties
            .Include(x=>x.Unit)
            .Include(x=>x.ProductCategory)
            .OrderBy(x=>x.ProductCategory.OrdinalNumber)
            .ThenBy(x=>x.OrdinalNo)
            .ThenBy(x=>x.PropertyNumber)
            .AsNoTracking()
            .AsQueryable();

        properties = Filter(properties, request.PropertyFilter);

        var propertiesList = await properties.ToListAsync();

        var vm = new PropertiesViewModel
            { 
              Properties = propertiesList,
              PropertyFilter = request.PropertyFilter,
              ProductCategories = await _context.ProductCategories.ToListAsync()
            };

        return vm;
           
    }

    public IQueryable<Property> Filter(IQueryable<Property> properties, PropertyFilter propertyFilter)
    {
        if (propertyFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(propertyFilter.Name))
                properties = properties.Where(x => x.Name.Contains(propertyFilter.Name));

            if (!string.IsNullOrWhiteSpace(propertyFilter.PropertyNumber))
                properties = properties.Where(x => x.PropertyNumber.Contains(propertyFilter.PropertyNumber));

            if (propertyFilter.ProductCategoryId != 0)
                properties = properties.Where(x => x.ProductCategoryId == propertyFilter.ProductCategoryId);
        }

        return properties;
    }
}
