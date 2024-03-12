using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsActiveSeparateUivs.Queries.GetIfsActiveSeparateUivs;

public class GetIfsActiveSeparateUivsQueryHandler : IRequestHandler<GetIfsActiveSeparateUivsQuery, IfsActiveSeparateUivsViewModel>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;

    public GetIfsActiveSeparateUivsQueryHandler(IOracleDbContext oracleContext, IApplicationDbContext context, IScadaIfsDbContext scadaContext)
    {
        _oracleContext = oracleContext;
        _context = context;
        _scadaContext = scadaContext;
    }
    public async Task<IfsActiveSeparateUivsViewModel> Handle(GetIfsActiveSeparateUivsQuery request, CancellationToken cancellationToken)
    {

        var ifsActiveSeparateUivs = _oracleContext.IfsActiveSeparateUivs
                   .FromSqlRaw(
                   @$"SELECT 
                         WO_NO WORK_NO,
                         Work_Center_Resource_API.Get_Mch_Work_Center(contract,mch_code) WORK_CENTER_NO,
                          MCH_CODE,
                          ORG_CODE,
                          Work_Center_API.Get_Department_No (contract,Work_Center_Resource_API.Get_Mch_Work_Center(contract,mch_code)) DEPARTMENT_NO,
                          ERR_DESCR,
                          CONTRACT,
                          STATE,
                          WORK_ORDER_SYMPT_CODE_API.Get_Description(ERR_SYMPTOM) WORK_ORDER_SYMPT_CODE,
                          REG_DATE,
                         (Select min(ACTUAL_START) from JT_TASK_TAB where WO_NO = asu.WO_NO) ACTUAL_START,
                         (Select max(ACTUAL_FINISH) from JT_TASK_TAB  where WO_NO = asu.WO_NO) ACTUAL_FINISH
                         FROM ifsapp.ACTIVE_SEPARATE_UIV asu WHERE STATE <> 'WorkDone'
                        ")
                        .AsNoTracking()
                        .AsQueryable();

        // and to_char(OP_START_DATE,'MM.DD.YYYY') = to_char(SYSDATE,'MM.DD.YYYY')


        var ifsActiveSeparateUivFilter = new IfsActiveSeparateUivFilter
        {
            WorkCenterNo = request.WorkCenterNo,
            DepartmentNo = request.DepartmentNo,
            OrgCode = request.OrgCode,
        };


        ifsActiveSeparateUivs = Filter(ifsActiveSeparateUivs, ifsActiveSeparateUivFilter);


        var ifsActiveSeparateUivsList = await ifsActiveSeparateUivs
            .OrderByDescending(x => x.REG_DATE)
            .ToListAsync();



        // filtruję po zmaterializowaniu się listy
        if (ifsActiveSeparateUivFilter != null)
        {
            if (ifsActiveSeparateUivFilter.OrgCode != null)
            {


                List<string> OrgCodes = ifsActiveSeparateUivFilter.OrgCode.Split(',').ToList();

                if (OrgCodes.Count() > 1)
                {
                    ifsActiveSeparateUivsList = ifsActiveSeparateUivsList
                        .Join(OrgCodes, u => u.ORG_CODE, o => o, (u, o) => u)
                        .ToList();
                }
            }
        }

        var vm = new IfsActiveSeparateUivsViewModel
        {
            IfsActiveSeparateUivFilter = ifsActiveSeparateUivFilter,
            IfsActiveSeparateUivs = ifsActiveSeparateUivsList
        };

        return vm;

    }


    private IQueryable<IfsActiveSeparateUiv> Filter(IQueryable<IfsActiveSeparateUiv> ifsActiveSeparateUivs, IfsActiveSeparateUivFilter ifsActiveSeparateUivFilter)
    {
        if (ifsActiveSeparateUivFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(ifsActiveSeparateUivFilter.WorkCenterNo))
                ifsActiveSeparateUivs = ifsActiveSeparateUivs.Where(x => x.WORK_CENTER_NO.Contains(ifsActiveSeparateUivFilter.WorkCenterNo.Trim()));

            if (!string.IsNullOrWhiteSpace(ifsActiveSeparateUivFilter.DepartmentNo))
                ifsActiveSeparateUivs = ifsActiveSeparateUivs.Where(x => x.DEPARTMENT_NO.Contains(ifsActiveSeparateUivFilter.DepartmentNo.Trim()));


            if (!string.IsNullOrWhiteSpace(ifsActiveSeparateUivFilter.OrgCode))
            {
                List<string> OrgCodes = ifsActiveSeparateUivFilter.OrgCode.Split(',').ToList();

                if (OrgCodes.Count() == 1)
                {
                    ifsActiveSeparateUivs = ifsActiveSeparateUivs.Where(x => x.ORG_CODE.Contains(OrgCodes[0].Trim()));
                }

            }
        }

        return ifsActiveSeparateUivs;
    }

}
