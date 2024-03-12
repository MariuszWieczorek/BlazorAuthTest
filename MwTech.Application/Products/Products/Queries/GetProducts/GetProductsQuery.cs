using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Application.Products.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.Products.Queries.GetProducts;

public class GetProductsQuery : IRequest<ProductsViewModel>
{
    public ProductFilter ProductFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string SearchValue { get; set; }
    public string OrderInfo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}
