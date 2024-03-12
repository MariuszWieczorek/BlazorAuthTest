using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.ProductCategories.Commands.EditProductCategory;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCategories.Queries.GetEditProductCategory;

public class GetEditProductCategoryQueryHandler : IRequestHandler<GetEditProductCategoryQuery, EditProductCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public GetEditProductCategoryQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditProductCategoryCommand> Handle(GetEditProductCategoryQuery request, CancellationToken cancellationToken)
    {

        var productCategoryToEdit =  _context.ProductCategories.Single(x => x.Id == request.Id);
        
        return new EditProductCategoryCommand 
        {
            Id = productCategoryToEdit.Id,
            Name = productCategoryToEdit.Name,
            CategoryNumber = productCategoryToEdit.CategoryNumber,
            Description = productCategoryToEdit.Description,
            OrdinalNumber = productCategoryToEdit.OrdinalNumber,
            TkwCountExcess = productCategoryToEdit.TkwCountExcess,
            NoCalculateTkw = productCategoryToEdit.NoCalculateTkw,
            TechCardNumber= productCategoryToEdit.TechCardNumber,
            RouteSource = productCategoryToEdit.RouteSource,
        };
    }
    
}
