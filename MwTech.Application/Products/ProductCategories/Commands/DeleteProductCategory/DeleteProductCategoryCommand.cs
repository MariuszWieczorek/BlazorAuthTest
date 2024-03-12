using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommand : IRequest
{
    public int Id { get; set; }
}
