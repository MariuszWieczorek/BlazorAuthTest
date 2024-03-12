using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductVersions.Commands.CopyProductVersion;

public class CopyProductVersionCommandHandler : IRequestHandler<CopyProductVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;

    public CopyProductVersionCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUser)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
    }

    public async Task Handle(CopyProductVersionCommand request, CancellationToken cancellationToken)
    {


        var product = await _context.Products
            .SingleAsync(x => x.Id == request.ProductId);



        await copyProductVersion(request);


        await _context.SaveChangesAsync();

        return;

    }

    private async Task copyProductVersion(CopyProductVersionCommand request)
    {
        var productVersion = await _context.ProductVersions
                    .SingleOrDefaultAsync(x => x.ProductId == request.ProductId && x.Id == request.ProductVersionId);




        var newProductVersion = new ProductVersion
        {
            ProductId = request.ProductId,
            VersionNumber = productVersion.VersionNumber,
            AlternativeNo = productVersion.AlternativeNo,   
            Name = productVersion.Name,
            Description = productVersion.Description,
            DefaultVersion = false,
            IfsDefaultVersion = false,
            ComarchDefaultVersion = false,
            ToIfs = productVersion.ToIfs,

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

            ProductQty = productVersion.ProductQty,
            ProductWeight = productVersion.ProductWeight,

            IsActive = true
        };


        _context.ProductVersions.Add(newProductVersion);

        await copyProductVersionBoms(request, newProductVersion);


    }





    private async Task copyProductVersionBoms(CopyProductVersionCommand request, ProductVersion newProductVersion)
    {
        var productBomsToCopy = await _context.Boms
                    .Where(x => x.SetId == request.ProductId && x.SetVersionId == request.ProductVersionId)
                    .AsNoTracking()
                    .ToListAsync();

        foreach (var bom in productBomsToCopy)
        {

            var newBom = new Bom
            {
                SetId = request.ProductId,
                SetVersion = newProductVersion,
                PartId = bom.PartId,
                PartQty = bom.PartQty,
                Excess = bom.Excess,
                OnProductionOrder = bom.OnProductionOrder,
                OrdinalNumber = bom.OrdinalNumber,
                DoNotExportToIfs = bom.DoNotExportToIfs,
                DoNotIncludeInTkw= bom.DoNotIncludeInTkw,
                DoNotIncludeInWeight= bom.DoNotIncludeInWeight,
                Layer= bom.Layer
            };

            _context.Boms.Add(newBom);

        }
    }

}
