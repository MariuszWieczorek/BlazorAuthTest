using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductCosts.Commands.AddProductCost;

public class AddProductCostCommandHandler : IRequestHandler<AddProductCostCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;

    public AddProductCostCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUser)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
    }

    public async Task Handle(AddProductCostCommand request, CancellationToken cancellationToken)
    {
        var productCost = new ProductCost
        {

            AccountingPeriodId = request.AccountingPeriodId,
            ProductId = request.ProductId,
            CurrencyId = request.CurrencyId,

            Cost = request.Cost,
            LabourCost = request.LabourCost,
            MaterialCost = request.MaterialCost,
            MarkupCost = request.MarkupCost,

            Description = request.Description,

            CreatedByUserId = _currentUser.UserId,
            CreatedDate = _dateTimeService.Now,


            EstimatedCost = request.EstimatedCost,
            EstimatedLabourCost = request.EstimatedLabourCost,
            EstimatedMaterialCost = request.EstimatedMaterialCost,
            EstimatedMarkupCost = request.EstimatedMarkupCost
        };

        await _context.ProductCosts.AddAsync(productCost);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
