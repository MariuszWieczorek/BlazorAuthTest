using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.SettingCategories.Commands.AddSettingCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.SettingCategories.Queries.GetAddSettingCategoryViewModel;

public class GetAddSettingCategoryViewModelQueryHandler : IRequestHandler<GetAddSettingCategoryViewModelQuery, AddSettingCategoryViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddSettingCategoryViewModelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AddSettingCategoryViewModel> Handle(GetAddSettingCategoryViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new AddSettingCategoryViewModel()
        {
            MachineCategories = await _context.MachineCategories.AsNoTracking().ToListAsync(),
            AddSettingCategoryCommand = new AddSettingCategoryCommand()
        };

        return vm;
    }
}
