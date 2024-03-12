using MediatR;
using MwTech.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.SettingCategories.Queries.GetSettingCategories;

public class GetSettingCategoriesQuery : IRequest<SettingCategoriesViewModel>
{
    public SettingCategoryFilter SettingCategoryFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public int Id { get; set; }
}
