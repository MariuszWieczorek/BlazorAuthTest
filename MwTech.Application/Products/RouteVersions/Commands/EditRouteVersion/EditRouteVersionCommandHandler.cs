using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.RouteVersions.Commands.EditRouteVersion;

public class EditRouteVersionCommandHandler : IRequestHandler<EditRouteVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditRouteVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditRouteVersionCommand request, CancellationToken cancellationToken)
    {
        var RouteVersion = await _context.RouteVersions
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.ProductId == request.ProductId);

        RouteVersion.VersionNumber = request.VersionNumber;
        RouteVersion.AlternativeNo = request.AlternativeNo;
        RouteVersion.Name = request.Name;    
        RouteVersion.Description = request.Description;
        RouteVersion.ProductQty = request.ProductQty;
        RouteVersion.IsActive = request.IsActive;
        RouteVersion.ToIfs = request.ToIfs;
        
      
        RouteVersion.ModifiedByUserId = _currentUserService.UserId;
        RouteVersion.ModifiedDate = _dateTimeService.Now;
        RouteVersion.ProductCategoryId = request.ProductCategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
