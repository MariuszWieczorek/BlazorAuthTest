using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.CopyProductPropertiesVersion;

public class CopyProductPropertiesVersionCommandHandler : IRequestHandler<CopyProductPropertiesVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;

    public CopyProductPropertiesVersionCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUser)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
    }

    public async Task Handle(CopyProductPropertiesVersionCommand request, CancellationToken cancellationToken)
    {

        await copyProductPropertiesVersion(request);
        await _context.SaveChangesAsync();

        return;

    }

    private async Task copyProductPropertiesVersion(CopyProductPropertiesVersionCommand request)
    {
        var productPropertiesVersion = await _context.ProductPropertyVersions
                    .SingleOrDefaultAsync(x => x.Id == request.ProductPropertiesVersionId);




        var newProductPropertiesVersion = new ProductPropertyVersion
        {
            ProductId = productPropertiesVersion.ProductId,
            VersionNumber = productPropertiesVersion.VersionNumber,
            Name = productPropertiesVersion.Name,
            Description = productPropertiesVersion.Description,
            DefaultVersion = false,


            IsAccepted01 = false,
            Accepted01ByUserId = null,
            Accepted01Date = null,

            IsAccepted02 = false,
            Accepted02ByUser = null,
            Accepted02Date = null,

            CreatedByUserId = _currentUser.UserId,
            CreatedDate = _dateTimeService.Now,

            ModifiedByUser = null,
            ModifiedDate = null,


            IsActive = false
        };


        _context.ProductPropertyVersions.Add(newProductPropertiesVersion);

        await copyProductPropertiesVersion(request, newProductPropertiesVersion);


    }





    private async Task copyProductPropertiesVersion(CopyProductPropertiesVersionCommand request, ProductPropertyVersion newProductPropertiesVersion)
    {
        //x.ProductId == request.ProductId &&

        var ProductPropertiesToCopy = await _context.ProductProperties
                    .Where(x => x.ProductPropertiesVersionId == request.ProductPropertiesVersionId)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var productProperty in ProductPropertiesToCopy)
        {

            var newProductProperty = new ProductProperty
            {
                ProductPropertiesVersion = newProductPropertiesVersion,
                PropertyId = productProperty.PropertyId,
                MinValue = productProperty.MinValue,
                Value = productProperty.Value,
                MaxValue = productProperty.MaxValue,
                Text = productProperty.Text
            };

            _context.ProductProperties.Add(newProductProperty);

        }
    }

}
