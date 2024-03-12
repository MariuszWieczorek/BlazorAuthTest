using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductCosts.Commands.CalculateProductsCosts;

public class CalculateProductsCostsCommandHandler : IRequestHandler<CalculateProductsCostsCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<CalculateProductsCostsCommandHandler> _logger;
    private readonly IProductCostService _productCostService;
    private readonly Stopwatch _timer;

    public CalculateProductsCostsCommandHandler(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<CalculateProductsCostsCommandHandler> logger,
        IProductCostService productCostService
        
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _productCostService = productCostService;
        _timer = new Stopwatch();
    }
    public async Task Handle(CalculateProductsCostsCommand request, CancellationToken cancellationToken)
    {

        var products = await _context.Products
                    .Include(x => x.ProductCategory)
                    .Where(y => y.ProductCategory.CategoryNumber == request.ProductCategoryNumber && y.IsActive)
                    .AsNoTracking()
                    .ToListAsync();
        
   

        int counter = 1;
        _timer.Start();
        foreach(var item in products)
        {

            if (counter % 100 == 0)
            {
                _logger.LogInformation($"TKW: {request.ProductCategoryNumber} rec: {counter}  czas: {_timer.ElapsedMilliseconds} ms");
                _context.Clear();
            }

            await _productCostService.CaluclateProductCost(item.Id);

            

            counter++;
        }

        /*
        Parallel.ForEach(products, async item =>
        {
             await _productCostService.CaluclateProductCost(item.Id);
        });
        */

        _timer.Stop();
        _logger.LogInformation($"TKW: {request.ProductCategoryNumber} rec: {counter}  czas: {_timer.ElapsedMilliseconds} ms");
        return;
    }

    
}
