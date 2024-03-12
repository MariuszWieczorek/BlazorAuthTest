using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MwTech.Application.Common.Models;

namespace MwTech.Application.Products.ProductCategories.Queries.GetProductCategories;

public class GetProductCategoriesQuery : IRequest<ProductCategoriesViewModel>
{
    public ProductCategoryFilter ProductCategoryFilter { get; set; }

    public PagingInfo PagingInfo { get; set; }

}
