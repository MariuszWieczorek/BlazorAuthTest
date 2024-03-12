using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.AddProduct;
using MwTech.Application.Products.RouteVersions.Commands.AddRouteVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.RouteVersions.Queries.GetAddRouteVersionViewModel;

public class GetAddRouteVersionViewModelQueryHandler : IRequestHandler<GetAddRouteVersionViewModelQuery, AddRouteVersionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public GetAddRouteVersionViewModelQueryHandler(IApplicationDbContext context,
          ICurrentUserService currentUserService,
          IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<AddRouteVersionViewModel> Handle(GetAddRouteVersionViewModelQuery request, CancellationToken cancellationToken)
    {

        int maxRouteVersionNumber = 1;
        int maxRouteAlternativeNo = 1;

        var numberOfRouteVersion = _context.RouteVersions
            .Where(x => x.ProductId == request.ProductId).Count();

        if(numberOfRouteVersion!=0)
        {
            maxRouteVersionNumber = _context.RouteVersions
                .Where(x => x.ProductId == request.ProductId)
                .Max(x=>x.VersionNumber) + 1;
        }
        
            
            

        var AddRouteVersionCommand = new AddRouteVersionCommand
        {
           ProductId = request.ProductId,
           VersionNumber = maxRouteVersionNumber,
           AlternativeNo = maxRouteAlternativeNo,
           Name = $"wersja {maxRouteVersionNumber}",
           Description = "",
           ProductQty = 1,
           IsActive = true,
           ToIfs = true,
           CreatedByUserId = _currentUserService.UserId,
           CreatedDate = _dateTimeService.Now,
           DefaultVersion = maxRouteVersionNumber == 1,
        };


        var vm = new AddRouteVersionViewModel
        {
            AddRouteVersionCommand = AddRouteVersionCommand,
            ProductCategories = await _context.ProductCategories.AsNoTracking().ToListAsync(),
        };
        return vm;
    }
}
