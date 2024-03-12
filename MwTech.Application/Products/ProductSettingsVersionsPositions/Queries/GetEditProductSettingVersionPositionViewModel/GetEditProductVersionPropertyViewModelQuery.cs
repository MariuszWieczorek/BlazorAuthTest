using MediatR;
using MwTech.Application.Settings.Queries.GetSettings;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Queries.GetEditProductSettingVersionPositionViewModel;

public class GetEditProductSettingVersionPositionViewModelQuery : IRequest<EditProductSettingVersionPositionViewModel>
{
    public int ProductSettingVersionId { get; set; }
    public int ProductId { get; set; }
    public int ProductSettingId { get; set; }
    public SettingFilter SettingFilter { get; set; }
    public string Tab { get; set; }
}
