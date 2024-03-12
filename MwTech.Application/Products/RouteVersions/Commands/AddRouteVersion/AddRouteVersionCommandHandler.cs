using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.RouteVersions.Commands.AddRouteVersion;

public class AddRouteVersionCommandHandler : IRequestHandler<AddRouteVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddRouteVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddRouteVersionCommand request, CancellationToken cancellationToken)
    {
        var RouteVersion = new RouteVersion
        {
            ProductId = request.ProductId,
            VersionNumber = request.VersionNumber,
            AlternativeNo = request.AlternativeNo,
            Name = request.Name,
            Description = request.Description,
            ProductQty = request.ProductQty,
            IsActive = request.IsActive,
            ToIfs = request.ToIfs,
            DefaultVersion = request.DefaultVersion,
            CreatedByUserId = _currentUserService.UserId,
            CreatedDate = _dateTimeService.Now,
            ProductCategoryId = request.ProductCategoryId
        };

        await _context.RouteVersions.AddAsync(RouteVersion);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
