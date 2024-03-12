using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MwTech.Application.Common.Models;

namespace MwTech.Application.MachineCategories.Queries.GetMachineCategories;

public class GetMachineCategoriesQuery : IRequest<MachineCategoriesViewModel>
{
    public MachineCategoryFilter MachineCategoryFilter { get; set; }

    public PagingInfo PagingInfo { get; set; }

}
