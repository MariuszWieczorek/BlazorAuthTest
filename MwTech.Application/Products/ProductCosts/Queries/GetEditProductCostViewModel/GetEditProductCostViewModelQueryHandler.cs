using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.ProductCosts.Commands.EditProductCost;

namespace MwTech.Application.Products.ProductCosts.Queries.GetEditProductCostViewModel;

public class GetEditProductCostViewModelQueryHandler : IRequestHandler<GetEditProductCostViewModelQuery, EditProductCostViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetEditProductCostViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EditProductCostViewModel> Handle(GetEditProductCostViewModelQuery request, CancellationToken cancellationToken)
    {

        var productCost = await _context.ProductCosts.SingleAsync(x => x.Id == request.Id);
        
        var editProductCostCommand = new EditProductCostCommand
        {
            Id = request.Id,
            ProductId = productCost.ProductId,
            AccountingPeriodId = productCost.AccountingPeriodId,
            CurrencyId = (int)productCost.CurrencyId,
            Cost = productCost.Cost,
            LabourCost = productCost.LabourCost,
            MaterialCost = productCost.MaterialCost,
            MarkupCost = productCost.MarkupCost,
            EstimatedCost = productCost.EstimatedCost,
            EstimatedLabourCost = productCost.EstimatedLabourCost,
            EstimatedMaterialCost = productCost.EstimatedMaterialCost,
            EstimatedMarkupCost = productCost.EstimatedMarkupCost
        };
        

        var vm = new EditProductCostViewModel()
        {
            AccountingPeriods = await _context.AccountingPeriods.AsNoTracking().ToListAsync(),
            Currencies = await _context.Currencies.AsNoTracking().ToListAsync(),
            EditProductCostCommand = editProductCostCommand
        };

        return vm;
    }
}
