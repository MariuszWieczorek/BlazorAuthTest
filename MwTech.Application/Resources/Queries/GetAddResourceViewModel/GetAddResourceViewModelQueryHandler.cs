using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Resources.Commands.AddResource;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Resources.Queries.GetAddResourceViewModel;

public class GetAddResourceViewModelQueryHandler : IRequestHandler<GetAddResourceViewModelQuery, AddResourceViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddResourceViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddResourceViewModel> Handle(GetAddResourceViewModelQuery request, CancellationToken cancellationToken)
    {
        var newResource = new AddResourceCommand()
        {
            Contract = "KT1"
        };


        var vm = new AddResourceViewModel()
        {
            ProductCategories = await _context.ProductCategories.AsNoTracking().ToListAsync(),
            Units = await _context.Units.AsNoTracking().ToListAsync(),
            AddResourceCommand = newResource
        };

        return vm;
    }
}
