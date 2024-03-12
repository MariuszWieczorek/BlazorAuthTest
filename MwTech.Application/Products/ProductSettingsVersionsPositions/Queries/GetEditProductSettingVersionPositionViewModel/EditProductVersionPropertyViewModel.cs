using MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.EditProductSettingVersionPosition;
using MwTech.Application.Settings.Queries.GetSettings;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Queries.GetEditProductSettingVersionPositionViewModel;

public class EditProductSettingVersionPositionViewModel
{
    public EditProductSettingVersionPositionCommand EditProductSettingVersionPositionCommand { get; set; }
    public SettingsViewModel SettingsViewModel { get; set; }
}
