using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Settings.Queries.GetEditSettingViewModel;

public class GetEditSettingViewModelQuery : IRequest<EditSettingViewModel>
{
    public int SettingId { get; set; }
    public string? Tab { get; set; }
}
