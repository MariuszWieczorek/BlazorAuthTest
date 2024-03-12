using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductCategories.Commands.AddProductCategory;

public class AddProductCategoryCommandHandler : IRequestHandler<AddProductCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public AddProductCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = new ProductCategory();


        productCategory.OrdinalNumber = request.OrdinalNumber;
        productCategory.Name = request.Name;
        productCategory.CategoryNumber = request.CategoryNumber;
        productCategory.Description = request.Description;
        productCategory.TkwCountExcess = request.TkwCountExcess;
        productCategory.NoCalculateTkw = request.NoCalculateTkw;
        productCategory.TechCardNumber = request.TechCardNumber;
        productCategory.RouteSource = request.RouteSource;

        await _context.ProductCategories.AddAsync(productCategory);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
