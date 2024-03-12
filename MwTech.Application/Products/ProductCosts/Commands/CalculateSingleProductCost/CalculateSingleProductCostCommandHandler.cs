using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductCosts.Commands.CalculateSingleProductCost;

public class CalculateSingleProductCostCommandHandler : IRequestHandler<CalculateSingleProductCostCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<CalculateSingleProductCostCommandHandler> _logger;
    private readonly IProductCostService _productCostService;

    public CalculateSingleProductCostCommandHandler(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<CalculateSingleProductCostCommandHandler> logger,
        IProductCostService productCostService
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _productCostService = productCostService;
    }
    public async Task Handle(CalculateSingleProductCostCommand request, CancellationToken cancellationToken)
    {
       await _productCostService.CaluclateProductCost(request.ProductId);
       return;
    }

    
}
