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

namespace MwTech.Application.Products.ProductProperties.Commands.AddProductProperties;

public class AddProductPropertiesCommandHandler : IRequestHandler<AddProductPropertiesCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddProductPropertiesCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }
    public async Task Handle(AddProductPropertiesCommand request, CancellationToken cancellationToken)
    {

        var productVersion = _context.ProductPropertyVersions
            .SingleOrDefault(x => x.Id == request.ProductPropertiesVersionId);


        if (productVersion != null)
        {



            var product = _context.Products.SingleOrDefault(x => x.Id == productVersion.ProductId);

            if (product != null)
            {

                var productProperties = await _context.ProductProperties
                    .Where(x => x.ProductPropertiesVersionId == request.ProductPropertiesVersionId)
                    .ToListAsync();



                var productPropertiesToAdd = await _context.Properties
                    .Where(x => x.ProductCategoryId == product.ProductCategoryId)
                    .ToListAsync();


                foreach (var item in productPropertiesToAdd)
                {
                    if (!productProperties.Where(x => x.PropertyId == item.Id).Any())
                    {
                        var productPropertyToAdd = new ProductProperty
                        {
                            ProductPropertiesVersionId = request.ProductPropertiesVersionId,
                            PropertyId = item.Id
                        };
                        await _context.ProductProperties.AddAsync(productPropertyToAdd);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        return;
    }
}
