using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsActiveSeparateUivs.Queries.GetIfsActiveSeparateUivs;

public class IfsActiveSeparateUivFilter
{
    public string WorkCenterNo { get; set; }
    public string DepartmentNo { get; set; }
    public string OrgCode { get; set; }

}
