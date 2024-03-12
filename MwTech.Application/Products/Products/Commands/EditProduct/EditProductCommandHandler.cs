using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Products.Commands.EditProduct;

public class EditProductCommandHandler : IRequestHandler<EditProductCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public EditProductCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);

        product.ProductNumber = request.ProductNumber;
        product.TechCardNumber = request.TechCardNumber;
        product.Name = request.Name;    
        product.Description = request.Description;
        product.ProductCategoryId = request.ProductCategoryId;
        product.UnitId = request.UnitId;
        product.ReturnedFromProd = request.ReturnedFromProd;
        product.NoCalculateTkw = request.NoCalculateTkw;
        product.IsActive = request.IsActive;
        product.IsTest = request.IsTest;
        product.OldProductNumber = request.OldProductNumber;
        product.MwbaseMatid = request.MwbaseMatid;
        product.MwbaseWyrobId = request.MwbaseWyrobId;
        product.ModifiedByUserId = _currentUserService.UserId;
        product.ModifiedDate = _dateTimeService.Now;
        product.Idx01 = request.Idx01;
        product.Idx02 = request.Idx02;
        product.ContentsOfRubber = request.ContentsOfRubber;
        product.Density = request.Density;
        product.ScalesId = request.ScalesId;
        product.Aps01 = request.Aps01;
        product.Aps02 = request.Aps02;
        product.DecimalPlaces  = request.DecimalPlaces;
        product.WeightTolerance = request.WeightTolerance;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
