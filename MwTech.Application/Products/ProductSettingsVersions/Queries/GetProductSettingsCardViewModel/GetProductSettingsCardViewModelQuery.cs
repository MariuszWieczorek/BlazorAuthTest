using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.ProductSettingsVersions.Queries.GetProductSettingsCardViewModel;

public class GetProductSettingsCardViewModelQuery : IRequest<ProductSettingsCardViewModel>
{
    public int? ProductSettingsVersionId { get; set; }
    public string? Tab { get; set; }
}
