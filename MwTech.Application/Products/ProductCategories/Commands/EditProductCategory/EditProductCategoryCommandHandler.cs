using MwTech.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MwTech.Application.Products.ProductCategories.Commands.EditProductCategory;
public class EditProductCategoryCommandHandler : IRequestHandler<EditProductCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public EditProductCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategoryToUpdate = await _context.ProductCategories
            .SingleAsync(x => x.Id == request.Id);

        productCategoryToUpdate.Name = request.Name;
        productCategoryToUpdate.OrdinalNumber = request.OrdinalNumber;
        productCategoryToUpdate.Description = request.Description;  
        productCategoryToUpdate.CategoryNumber = request.CategoryNumber;
        productCategoryToUpdate.TkwCountExcess = request.TkwCountExcess;
        productCategoryToUpdate.NoCalculateTkw = request.NoCalculateTkw;
        productCategoryToUpdate.TechCardNumber= request.TechCardNumber;
        productCategoryToUpdate.RouteSource= request.RouteSource;


        _context.ProductCategories.Update(productCategoryToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
