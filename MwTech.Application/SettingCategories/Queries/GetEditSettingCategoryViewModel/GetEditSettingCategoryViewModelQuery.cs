using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.SettingCategories.Queries.GetEditSettingCategoryViewModel;

public class GetEditSettingCategoryViewModelQuery : IRequest<EditSettingCategoryViewModel>
{
    public int Id { get; set; }
}
