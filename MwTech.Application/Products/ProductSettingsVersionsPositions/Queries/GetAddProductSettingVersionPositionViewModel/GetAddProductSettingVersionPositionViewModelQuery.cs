using MediatR;
using MwTech.Application.Settings.Queries.GetSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Queries.GetAddProductSettingVersionPositionViewModel;

public class GetAddProductSettingVersionPositionViewModelQuery : IRequest<AddProductSettingVersionPositionViewModel>
{
    public int ProductSettingVersionId { get; set; }
    public int ProductId { get; set; }
    public SettingFilter SettingFilter { get; set; }
    public string Tab { get; set; }
}
