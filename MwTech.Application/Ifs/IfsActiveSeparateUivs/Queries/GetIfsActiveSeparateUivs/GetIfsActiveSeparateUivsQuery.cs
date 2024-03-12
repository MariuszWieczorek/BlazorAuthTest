using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsActiveSeparateUivs.Queries.GetIfsActiveSeparateUivs;

public class GetIfsActiveSeparateUivsQuery : IRequest<IfsActiveSeparateUivsViewModel>
{
    public string WorkCenterNo { get; set; }
    public string DepartmentNo { get; set; }
    public string OrgCode { get; set; }
}
