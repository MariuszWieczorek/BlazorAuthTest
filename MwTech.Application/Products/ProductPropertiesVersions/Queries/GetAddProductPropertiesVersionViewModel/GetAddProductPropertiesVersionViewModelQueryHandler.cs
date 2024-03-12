using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.AddProduct;
using MwTech.Application.Products.ProductPropertiesVersions.Commands.AddProductPropertiesVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductPropertiesVersions.Queries.GetAddProductPropertiesVersionViewModel;

public class GetAddProductPropertiesVersionViewModelQueryHandler : IRequestHandler<GetAddProductPropertiesVersionViewModelQuery, AddProductPropertiesVersionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public GetAddProductPropertiesVersionViewModelQueryHandler(IApplicationDbContext context,
          ICurrentUserService currentUserService,
          IDateTimeService dateTimeService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<AddProductPropertiesVersionViewModel> Handle(GetAddProductPropertiesVersionViewModelQuery request, CancellationToken cancellationToken)
    {

        int maxProductPropertiesVersionNumber = 1;
        int maxAlternativeNo = 1;

        var numberOfProductPropertiesVersion = _context.ProductPropertyVersions
            .Where(x => x.ProductId == request.ProductId).Count();

        if(numberOfProductPropertiesVersion!=0)
        {
            maxProductPropertiesVersionNumber = _context.ProductPropertyVersions
                .Where(x => x.ProductId == request.ProductId)
                .Max(x=>x.VersionNumber) + 1;
        }
        
            
            

        var AddProductPropertiesVersionCommand = new AddProductPropertiesVersionCommand
        {
           ProductId = request.ProductId,
           VersionNumber = maxProductPropertiesVersionNumber,
           AlternativeNo = maxAlternativeNo,
           Name = $"wersja {maxProductPropertiesVersionNumber}",
           Description = "",
           ProductQty = 1,
           IsActive = true,
           ToIfs = true,
           CreatedByUserId = _currentUserService.UserId,
           CreatedDate = _dateTimeService.Now,
           DefaultVersion = maxProductPropertiesVersionNumber == 1,
        };


        var vm = new AddProductPropertiesVersionViewModel
        {
            AddProductPropertiesVersionCommand = AddProductPropertiesVersionCommand,
        };
        return vm;
    }
}
