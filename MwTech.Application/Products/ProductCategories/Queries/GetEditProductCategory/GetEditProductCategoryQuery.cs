using MediatR;
using MwTech.Application.Products.ProductCategories.Commands.EditProductCategory;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.ProductCategories.Queries.GetEditProductCategory;

public class GetEditProductCategoryQuery : IRequest<EditProductCategoryCommand> 
{
    public int Id { get; set; }
}
