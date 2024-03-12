using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Ifs.IfsSourceRoutes.Queries.CompareRoutesIfsWithMwTech;

public class CompareRoutesIfsWithMwTechCommandHandler : IRequestHandler<CompareRoutesIfsWithMwTechCommand, CompareRoutesIfsWithMwTechViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IOracleDbContext _oracle;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<CompareRoutesIfsWithMwTechCommandHandler> _logger;
    private readonly IProductCsvService _productCsvService;

    public CompareRoutesIfsWithMwTechCommandHandler(IApplicationDbContext context,
        IOracleDbContext oracle,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<CompareRoutesIfsWithMwTechCommandHandler> logger,
        IProductCsvService productCsvService
        )
    {
        _context = context;
        _oracle = oracle;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _productCsvService = productCsvService;
    }
    public async Task<CompareRoutesIfsWithMwTechViewModel> Handle(CompareRoutesIfsWithMwTechCommand request, CancellationToken cancellationToken)
    {
        int idxNo = request.IdxNo;

        string mwtechRoutesView = "dbo.MWTech_route_IFS";
        string additionalCondition = string.Empty;

        if (idxNo == 2)
        {
            mwtechRoutesView = "dbo.MWTech_route_IFS_idx02";
            additionalCondition = "AND ifs.RevisionNo >= '5'";

        }
        if (idxNo == 1)
        {
            mwtechRoutesView = "dbo.MWTech_route_IFS_idx01";
            additionalCondition = "AND ifs.RevisionNo >= '5'";
        }

        IQueryable<ComparedRouteIfsVsMwTech> comparedRoutes = _context.ComparedRoutesIfsVsMwTech
           .FromSqlRaw($@"
            SELECT *
            FROM (
            SELECT 
             MWT.CategoryNumber
            ,MWT.Idx as Idx
            ,{idxNo} as IdxNo
            ,IFS.PartNo as PartNo
            ,IFS.RevisionNo as RevisionNo
            ,IFS.AlternativeNo as AlternativeNo
            ,IFS.OperationNo as OperationNo
            --
            ,ISNULL(IFS.AlternativeDescription,'') as IfsAlternativeDescription
            ,ISNULL(MWT.ALTERNATIVE_DESCRIPTION,'') as MwtAlternativeDescription
            --
            ,ISNULL(IFS.OperationDescription,'') as IfsOperationDescription
            ,ISNULL(MWT.OPERATION_DESCRIPTION,'') as MwtOperationDescription
            --
            ,ISNULL(IFS.WorkCenterNo,'') as IfsWorkCenterNo
            ,ISNULL(MWT.WORK_CENTER_NO,'') as MwtWorkCenterNo
            --
            ,ISNULL(IFS.LaborClassNo,'') as IfsLaborClassNo
            ,ISNULL(MWT.LABOR_CLASS_NO,'') as MwtLaborClassNo
            --
            ,ISNULL(IFS.LaborRunFactor,0) as IfsLaborRunFactor
            ,ISNULL(MWT.LABOR_RUN_FACTOR,0) as MwtLaborRunFactor
            --
            ,ISNULL(IFS.MachRunFactor,0) as IfsMachRunFactor
            ,ISNULL(MWT.MACH_RUN_FACTOR,0) as MwtMachRunFactor
            --
            ,ISNULL(IFS.CrewSize,0) as IfsCrewSize
            ,ISNULL(MWT.CREW_SIZE,0) as MwtCrewSize
            --
            ,ISNULL(IFS.SetupLaborClassNo,'') as IfsSetupLaborClassNo
            ,ISNULL(MWT.SETUP_LABOR_CLASS_NO,'') as MwtSetupLaborClassNo
            --
            ,ISNULL(IFS.SetupCrewSize,0) as IfsSetupCrewSize
            ,ISNULL(MWT.SETUP_CREW_SIZE,0) as MwtSetupCrewSize
            --
            ,ISNULL(IFS.MachSetupTime,0) as IfsMachSetupTime
            ,ISNULL(MWT.MACH_SETUP_TIME,0) as MwtMachSetupTime
            --
            ,ISNULL(IFS.LaborSetupTime,0) as IfsLaborSetupTime
            ,ISNULL(MWT.LABOR_SETUP_TIME,0) as MwtLaborSetupTime
            --
            ,ISNULL(IFS.MoveTime,0) as IfsMoveTime
            ,ISNULL(MWT.MOVE_TIME,0) as MwtMoveTime
            --
            ,ISNULL(IFS.Overlap,0) as IfsOverlap
            ,ISNULL(MWT.OVERLAP,0) as MwtOverlap
            --
            ,ISNULL(IFS.RunTimeCode,'') as IfsRunTimeCode
            ,ISNULL(MWT.RUN_TIME_CODE,'') as MwtRunTimeCode
            -- 
            ,ISNULL(IFS.OperationId,0) as IfsOperationId    
            --
            , ISNULL(IFS.ToolId,'') as IfsToolId
            , ISNULL(MWT.TOOL_ID,'')  as MwtToolId
            , ISNULL(IFS.ToolQuantity,0) as  IfsToolQuantity
            , ISNULL(MWT.TOOL_QUANTITY,0) as  MwtToolQuantity         
            --
            ,MWT.RoutePositionId
            --
            ,IIF(MWT.OPERATION_NO IS NULL,0,1) as TestLineExists
            --
            ,IIF( IFS.OperationDescription = MWT.OPERATION_DESCRIPTION,1,0) as OperationDescriptionTest
            ,IIF( IFS.WorkCenterNo = MWT.WORK_CENTER_NO,1,0) as WorkCenterNoTest
            ,IIF( IFS.LaborClassNo = MWT.LABOR_CLASS_NO,1,0) as LaborClassNoTest
            ,IIF( IFS.CrewSize = MWT.CREW_SIZE,1,0) as CrewSizeTest
            ,IIF( IFS.MachRunFactor = ROUND(MWT.MACH_RUN_FACTOR,5),1,0) as MachRunFactorTest
            ,IIF( IFS.LaborRunFactor = ROUND(MWT.LABOR_RUN_FACTOR,5),1,0) as LaborRunFactorTest
            --
            ,IIF( ISNULL(IFS.SetupCrewSize,0) = ISNULL(MWT.SETUP_CREW_SIZE,0) ,1,0) as SetupCrewSizeTest
            ,IIF( ISNULL(IFS.SetupLaborClassNo,'') = ISNULL(MWT.SETUP_LABOR_CLASS_NO,'') ,1,0) as SetupLaborClassNoTest
            ,IIF( ISNULL(IFS.MachSetupTime,0) = ISNULL(MWT.MACH_SETUP_TIME,0),1,0) as MachSetupTimeTest
            ,IIF( ISNULL(IFS.LaborSetupTime,0) = ISNULL(MWT.LABOR_SETUP_TIME,0) ,1,0) as LaborSetupTimeTest
            ,IIF( ISNULL(IFS.Overlap,0) = ISNULL(MWT.OVERLAP,0) ,1,0) as OverlapTest
            ,IIF( ISNULL(IFS.MoveTime,0) = ISNULL(MWT.MOVE_TIME,0) ,1,0) as MoveTimeTest
            ,IIF( ISNULL(IFS.ToolId,'') = ISNULL(MWT.TOOL_ID,'') ,1,0) as ToolIdTest
            ,IIF( ISNULL(IFS.ToolQuantity,0) = ISNULL(MWT.TOOL_QUANTITY,0) ,1,0) as ToolQuantityTest
            --
            FROM dbo.IfsRoutes as IFS
            --
            left join dbo.Products as pr
            on pr.ProductNumber = IFS.PartNo
            left join dbo.ProductCategories as ca
            on ca.Id = pr.ProductCategoryId
            --
            left join {mwtechRoutesView} as MWT
            ON  MWT.PART_NO = IFS.PartNo
            AND MWT.ROUTING_REVISION = IFS.RevisionNo
            AND MWT.ALTERNATIVE_NO = IFS.AlternativeNo 
            AND MWT.OPERATION_NO = IFS.OperationNo
            WHERE 1 = 1 
            AND (ISNUMERIC(ifs.AlternativeNo ) = 1 OR ifs.AlternativeNo = '*')
            AND (ISNUMERIC(ifs.RevisionNo ) = 1 OR ifs.RevisionNo = '*')
            {additionalCondition}
            AND ca.RouteSource = {idxNo}
            -- AND ifs.PartNo not in (select ProductNumber from dbo.IFS_black_list)
            ) AS x
            WHERE 1 = 1
            -- AND x.RoutePositionId IS NOT NULL
            AND x.PartNo not in (select ProductNumber from dbo.IFS_black_list)
            AND
            (	
	             x.WorkCenterNoTest = 0
             OR  x.LaborClassNoTest = 0
             OR  x.CrewSizeTest = 0
             OR  x.MachRunFactorTest = 0
             OR  x.LaborRunFactorTest = 0
 
             OR  x.SetupCrewSizeTest = 0
             OR  x.SetupLaborClassNoTest = 0
             OR  x.OverlapTest = 0
             OR  x.MoveTimeTest = 0
             OR  x.MachSetupTimeTest = 0
             OR  x.LaborSetupTimeTest = 0
            
             -- OR x.ToolIdTest = 0
             -- OR x.ToolQuantityTest = 0
            )
            AND x.PartNo not like 'KO%G2' -- odrzucamy drugie gatunki
            AND x.PartNo not like 'MEM-%' -- odrzucamy membrany
            AND x.PartNo not like '%-TE%' -- odrzucamy testowe

           "
           )
         .AsNoTracking()
         .AsQueryable();


        comparedRoutes = Filter(comparedRoutes, request.CompareRouteFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {


            request.PagingInfo.ItemsPerPage = 100;
            /*
            if (request.PagingInfo.ItemsPerPage > 0)
                comparedRoutes = comparedRoutes
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
            */
        }

        var ifsSourceRoutesList = await comparedRoutes
            .OrderBy(x => x.PartNo)
            .ThenBy(x => x.RevisionNo)
            .ThenBy(x => x.AlternativeNo)
            .ThenBy(x => x.OperationNo)
            .ToListAsync();

        request.PagingInfo.TotalItems = ifsSourceRoutesList.Count();

        var compareRoutesIfsWithMwTechViewModel = new CompareRoutesIfsWithMwTechViewModel
        {
            ComparedRoutesIfsVsMwTech = ifsSourceRoutesList,
            PagingInfo = request.PagingInfo,
            CompareRouteFilter = request.CompareRouteFilter
        };

        return compareRoutesIfsWithMwTechViewModel;
    }


    public IQueryable<ComparedRouteIfsVsMwTech> Filter(IQueryable<ComparedRouteIfsVsMwTech> comparedRoutes, CompareRouteFilter filter)
    {
        if (filter != null)
        {

            if (filter.ProductNumber != null)
                comparedRoutes = comparedRoutes.Where(x => x.PartNo.Contains(filter.ProductNumber));
        }

        return comparedRoutes;
    }


}
