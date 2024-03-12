using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingVersions.Commands.AddProductSettingVersion;

public class AddProductSettingVersionCommandHandler : IRequestHandler<AddProductSettingVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddProductSettingVersionCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddProductSettingVersionCommand request, CancellationToken cancellationToken)
    {
        string versionName = CreateVersionName(request);


        // request.Name

        var productSettingVersion = new ProductSettingVersion
        {
            ProductSettingVersionNumber = request.ProductSettingVersionNumber,
            AlternativeNo = request.AlternativeNo,
            Name = versionName,
            Description = request.Description,
            MachineCategoryId = request.MachineCategoryId,
            MachineId = request.MachineId,
            WorkCenterId = request.WorkCenterId,
            ProductId = request.ProductId,
            MwbaseId = request.MwbaseId,
            CreatedByUserId = _currentUserService.UserId,
            CreatedDate = _dateTimeService.Now,
            IsActive = request.IsActive,
        };

        await _context.ProductSettingVersions.AddAsync(productSettingVersion);

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }

    private string CreateVersionName(AddProductSettingVersionCommand request)
    {
        var product = _context.Products
            .Include(x=>x.ProductCategory)
            .SingleOrDefault(x => x.Id == request.ProductId);
        
        var machine = _context.Machines.SingleOrDefault(x => x.Id == request.MachineId);
        
        var versionName = $"{product.ProductCategory.CategoryNumber}-{product.TechCardNumber}-{machine.MachineNumber}-{request.ProductSettingVersionNumber}";
        
        return versionName;
    }
}
