using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductVersions.Commands.AddProductVersion;

public class AddProductVersionCommandHandler : IRequestHandler<AddProductVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddProductVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddProductVersionCommand request, CancellationToken cancellationToken)
    {
        var productVersion = new ProductVersion
        {
            ProductId = request.ProductId,
            VersionNumber = request.VersionNumber,
            AlternativeNo = request.AlternativeNo,
            Name = request.Name,
            Description = request.Description,
            ProductQty = request.ProductQty,
            ProductWeight = request.ProductWeight,
            IsActive = request.IsActive,
            ToIfs = request.ToIfs,
            CreatedByUserId = _currentUserService.UserId,
            CreatedDate = _dateTimeService.Now,
            DefaultVersion = request.DefaultVersion
        };

        await _context.ProductVersions.AddAsync(productVersion);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
