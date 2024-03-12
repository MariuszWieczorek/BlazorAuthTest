using MediatR;
using MwTech.Application.Settings.Queries.GetSettings;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Queries.GetCopyProductSettingVersionPositionsViewModel;

public class GetCopyProductSettingVersionPositionsViewModelQuery : IRequest<CopyProductSettingVersionPositionsViewModel>
{
    public int ProductSettingVersionId { get; set; }
    public int ProductId { get; set; }

}
