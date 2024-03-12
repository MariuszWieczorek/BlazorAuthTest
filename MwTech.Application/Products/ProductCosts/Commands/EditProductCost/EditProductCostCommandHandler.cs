using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductCosts.Commands.EditProductCost;

public class EditProductCostCommandHandler : IRequestHandler<EditProductCostCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public EditProductCostCommandHandler(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(EditProductCostCommand request, CancellationToken cancellationToken)
    {
        var productCost = await _context.ProductCosts.FirstOrDefaultAsync(x => x.Id == request.Id);

        productCost.CurrencyId = request.CurrencyId;
        productCost.Cost = request.Cost;
        productCost.LabourCost = request.LabourCost;
        productCost.MaterialCost = request.MaterialCost;
        productCost.MarkupCost = request.MarkupCost;
        productCost.EstimatedCost = request.EstimatedCost;
        productCost.EstimatedLabourCost = request.EstimatedLabourCost;
        productCost.EstimatedMaterialCost = request.EstimatedMaterialCost;
        productCost.EstimatedMarkupCost = request.EstimatedMarkupCost;
        productCost.ModifiedDate = _dateTimeService.Now;
        productCost.ModifiedByUserId = _currentUserService.UserId;     
  
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
