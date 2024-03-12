using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.AddProduct;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Products.Commands.EditProduct;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AddProductCommandHandler(IApplicationDbContext context
        , ICurrentUserService currentUserService
        , IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
    {

        var product = new Product
        {

            ProductNumber = request.ProductNumber,
            TechCardNumber = request.TechCardNumber,
            Name = request.Name,
            Description = request.Description,
            ProductCategoryId = request.ProductCategoryId,
            UnitId = request.UnitId,
            ReturnedFromProd = request.ReturnedFromProd,
            NoCalculateTkw = request.NoCalculateTkw,
            IsActive = request.IsActive,
            IsTest = request.IsTest,    
            OldProductNumber = request.OldProductNumber,
            MwbaseMatid = request.MwbaseMatid,
            MwbaseWyrobId = request.MwbaseWyrobId,
            CreatedByUserId = _currentUserService.UserId,
            CreatedDate = _dateTimeService.Now,
            ContentsOfRubber = request.ContentsOfRubber,
            Density = request.Density,
            ScalesId = request.ScalesId,
            Aps01= request.Aps01,
            Aps02= request.Aps02,
            DecimalPlaces = request.DecimalPlaces,
            WeightTolerance = request.WeightTolerance,
    };

        _context.Products.Add(product); 
        
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
