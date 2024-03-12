using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;

namespace MwTech.Application.ProductSettingsVersions.Queries.GetProductsSettingsVersions;

public class GetProductsSettingsVersionsQuery : IRequest<ProductsSettingVersionsViewModel>
{
    public ProductSettingVersionFilter ProductSettingVersionFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string SearchValue { get; set; }
    public string OrderInfo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}
