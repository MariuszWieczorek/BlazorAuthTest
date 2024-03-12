using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductPropertiesVersions.Commands.EditProductPropertiesVersion;

public class EditProductPropertiesVersionCommandHandler : IRequestHandler<EditProductPropertiesVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditProductPropertiesVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditProductPropertiesVersionCommand request, CancellationToken cancellationToken)
    {
        var ProductPropertiesVersion = await _context.ProductPropertyVersions
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.ProductId == request.ProductId);

        ProductPropertiesVersion.VersionNumber = request.VersionNumber;
        ProductPropertiesVersion.AlternativeNo = request.AlternativeNo;
        ProductPropertiesVersion.Name = request.Name;    
        ProductPropertiesVersion.Description = request.Description;
        ProductPropertiesVersion.IsActive = request.IsActive;
        ProductPropertiesVersion.ModifiedByUserId = _currentUserService.UserId;
        ProductPropertiesVersion.ModifiedDate = _dateTimeService.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
