using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.AddProductPropertiesVersion;

public class AddProductPropertiesVersionCommandHandler : IRequestHandler<AddProductPropertiesVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddProductPropertiesVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddProductPropertiesVersionCommand request, CancellationToken cancellationToken)
    {
        var ProductPropertiesVersion = new ProductPropertyVersion
        {
            ProductId = request.ProductId,
            VersionNumber = request.VersionNumber,
            AlternativeNo = request.AlternativeNo,
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive,
            DefaultVersion = request.DefaultVersion,
            CreatedByUserId = _currentUserService.UserId,
            CreatedDate = _dateTimeService.Now
        };

        await _context.ProductPropertyVersions.AddAsync(ProductPropertiesVersion);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
