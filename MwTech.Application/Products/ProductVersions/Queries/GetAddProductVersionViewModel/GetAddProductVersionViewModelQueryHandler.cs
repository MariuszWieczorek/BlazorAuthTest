using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.AddProduct;
using MwTech.Application.Products.ProductVersions.Commands.AddProductVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductVersions.Queries.GetAddProductVersionViewModel;

public class GetAddProductVersionViewModelQueryHandler : IRequestHandler<GetAddProductVersionViewModelQuery, AddProductVersionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public GetAddProductVersionViewModelQueryHandler(IApplicationDbContext context,
          ICurrentUserService currentUserService,
          IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<AddProductVersionViewModel> Handle(GetAddProductVersionViewModelQuery request, CancellationToken cancellationToken)
    {

        int maxProductVersionNumber = 1;
        int maxProductAlternativeNo = 1;

        var numberOfRouteVersion = _context.ProductVersions
            .Where(x => x.ProductId == request.ProductId).Count();

        if (numberOfRouteVersion != 0)
        {
            maxProductVersionNumber = _context.ProductVersions
                .Where(x => x.ProductId == request.ProductId)
                .Max(x => x.VersionNumber) + 1;
        }


        var AddProductVersionCommand = new AddProductVersionCommand
        {
           ProductId = request.ProductId,
           VersionNumber = maxProductVersionNumber,
           AlternativeNo = maxProductAlternativeNo,
           Name = $"wersja {maxProductVersionNumber}",
           Description = "",
           ProductQty = 1,
           ProductWeight = 0,
           IsActive = true,
           ToIfs = true,
           IfsDefaultVersion = maxProductVersionNumber == 1,
           CreatedByUserId = _currentUserService.UserId,
           CreatedDate = _dateTimeService.Now
        };


        var vm = new AddProductVersionViewModel
        {
            AddProductVersionCommand = AddProductVersionCommand,
        };
        return vm;
    }
}
