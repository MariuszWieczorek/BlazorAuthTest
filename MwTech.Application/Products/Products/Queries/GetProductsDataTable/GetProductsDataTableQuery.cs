using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.Products.Queries.GetProductsDataTable;

public class GetProductsDataTableQuery : IRequest<GetProductsDataTableViewModel>
{
    public ProductDataTableFilter ProductFilter { get; set; }
    public string SearchValue { get; set; }
    public string OrderInfo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}
