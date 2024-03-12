using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.ProductCosts.Commands.AddProductCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Queries.GetAddProductCostViewModel;

public class GetAddProductCostViewModelQueryHandler : IRequestHandler<GetAddProductCostViewModelQuery, AddProductCostViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddProductCostViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddProductCostViewModel> Handle(GetAddProductCostViewModelQuery request, CancellationToken cancellationToken)
    {


        var vm = new AddProductCostViewModel()
        {
            AccountingPeriods = await _context.AccountingPeriods.AsNoTracking().ToListAsync(),
            Currencies = await _context.Currencies.AsNoTracking().ToListAsync(),
            AddProductCostCommand = new AddProductCostCommand 
            {   CurrencyId = request.CurrencyId,
                AccountingPeriodId = request.PeriodId,
                ProductId = request.ProductId,
            },
        };

        return vm;
    }
}
