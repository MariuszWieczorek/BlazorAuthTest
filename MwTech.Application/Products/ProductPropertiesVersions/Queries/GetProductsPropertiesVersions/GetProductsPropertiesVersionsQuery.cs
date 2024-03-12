using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;

namespace MwTech.Application.GetProductsPropertiesVersions.Queries.GetProductsPropertiesVersions;

public class GetProductsPropertiesVersionsQuery : IRequest<ProductsPropertiesVersionsViewModel>
{
    public ProductPropertyVersionFilter ProductPropertyVersionFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string SearchValue { get; set; }
    public string OrderInfo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}
