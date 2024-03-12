using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsActiveSeparateUivs.Queries.GetIfsActiveSeparateUivs;

public class IfsActiveSeparateUivsViewModel
{
    public IfsActiveSeparateUivFilter IfsActiveSeparateUivFilter { get; set; }
    public List<IfsActiveSeparateUiv> IfsActiveSeparateUivs { get; set; }
}
