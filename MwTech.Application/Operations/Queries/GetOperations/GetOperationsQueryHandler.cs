using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Operations.Queries.GetOperations;

public class GetOperationsQueryHandler : IRequestHandler<GetOperationsQuery, GetOperationsViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetOperationsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<GetOperationsViewModel> Handle(GetOperationsQuery request, CancellationToken cancellationToken)
    {
        var operations = _context.Operations
            .Include(x=>x.Unit)
            .Include(x=>x.ProductCategory)
            .Include(x=>x.ManufactoringRoutes)
            .AsNoTracking()
            .AsQueryable();

        operations = Filter(operations, request.OperationFilter);

        var OperationsList = await operations
            .OrderBy(x=>x.OperationNumber)
            .ToListAsync();

        var vm = new GetOperationsViewModel
            { 
              Operations = OperationsList,
              OperationFilter = request.OperationFilter,
              ProductCategories = await _context.ProductCategories.ToListAsync()
            };

        return vm;
           
    }

    public IQueryable<Operation> Filter(IQueryable<Operation> operations, OperationFilter operationFilter)
    {
        if (operationFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(operationFilter.Name))
                operations = operations.Where(x => x.Name.Contains(operationFilter.Name));

            if (!string.IsNullOrWhiteSpace(operationFilter.OperationNumber))
                operations = operations.Where(x => x.OperationNumber.Contains(operationFilter.OperationNumber));

            if (operationFilter.ProductCategoryId != 0)
                operations = operations.Where(x => x.ProductCategoryId == operationFilter.ProductCategoryId);
        }

        return operations;
    }
}
