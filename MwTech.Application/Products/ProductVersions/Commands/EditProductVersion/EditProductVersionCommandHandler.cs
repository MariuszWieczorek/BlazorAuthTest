using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductVersions.Commands.EditProductVersion;

public class EditProductVersionCommandHandler : IRequestHandler<EditProductVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditProductVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditProductVersionCommand request, CancellationToken cancellationToken)
    {
        var productVersion = await _context.ProductVersions
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.ProductId == request.ProductId);

        productVersion.VersionNumber = request.VersionNumber;
        productVersion.AlternativeNo = request.AlternativeNo;
        productVersion.Name = request.Name;    
        productVersion.Description = request.Description;
        productVersion.ProductQty = request.ProductQty;
        productVersion.ProductWeight = request.ProductWeight;
        productVersion.IsActive = request.IsActive;
        productVersion.ToIfs = request.ToIfs;
      
        productVersion.ModifiedByUserId = _currentUserService.UserId;
        productVersion.ModifiedDate = _dateTimeService.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
