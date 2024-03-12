using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;

namespace MwTech.Application.Settings.Queries.GetSettings;

public class GetSettingsQuery : IRequest<SettingsViewModel>
{
    public SettingFilter SettingFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string SearchValue { get; set; }
    public string OrderInfo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}
