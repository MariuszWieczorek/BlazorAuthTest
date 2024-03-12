using EFCore.BulkExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Ifs.IfsSourceRoutes.Command.ImportIfsSourceRoutes;

public class ImportIfsSourceRoutesCommandHandler : IRequestHandler<ImportIfsSourceRoutesCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IOracleDbContext _oracle;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<ImportIfsSourceRoutesCommandHandler> _logger;
    private readonly IProductCsvService _productCsvService;

    public ImportIfsSourceRoutesCommandHandler(IApplicationDbContext context,
        IOracleDbContext oracle,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<ImportIfsSourceRoutesCommandHandler> logger,
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
    public async Task Handle(ImportIfsSourceRoutesCommand request, CancellationToken cancellationToken)
    {

            var ifsSourceRoutes = await _oracle.IfsSourceRoutes
           .FromSqlRaw($@"SELECT
              r.CONTRACT as Contract
            , r.PART_NO as PartNo
            , r.ALTERNATIVE_NO as AlternativeNo
            , r.ROUTING_REVISION as RevisionNo
            , r.OPERATION_NO as OperationNo
            , r.OPERATION_DESCRIPTION as OperationDescription
            , 'x' as AlternativeDescription
            , r.WORK_CENTER_NO as WorkCenterNo
            , r.MACH_RUN_FACTOR as MachRunFactor
            , r.LABOR_CLASS_NO as LaborClassNo
            , r.LABOR_RUN_FACTOR as LaborRunFactor
            , r.CREW_SIZE as CrewSize
            , r.RUN_TIME_CODE as RunTimeCode
            , r.SETUP_LABOR_CLASS_NO as SetupLaborClassNo
            , r.MACH_SETUP_TIME as MachSetupTime
            , r.LABOR_SETUP_TIME as LaborSetupTime
            , r.SETUP_CREW_SIZE as SetupCrewSize
            , r.MOVE_TIME as MoveTime
            , r.OVERLAP as Overlap
            , r.OPERATION_ID as OperationId
            -- , t.TOOL_ID as ToolId
            -- , t.TOOL_QUANTITY as ToolQuantity
            , '' as ToolId
            , 0 as ToolQuantity
            FROM ROUTING_OPERATION r
            INNER JOIN ROUTING_HEAD h
            ON  r.CONTRACT = h.CONTRACT
            AND r.PART_NO = h.PART_NO
            AND r.ROUTING_REVISION = h.ROUTING_REVISION
            AND r.BOM_TYPE_DB = h.BOM_TYPE_DB 
            --
            -- LEFT JOIN ROUTING_OPERATION_TOOL t
            -- ON  t.CONTRACT = h.CONTRACT
            -- AND t.PART_NO = h.PART_NO
            -- AND t.ROUTING_REVISION = h.ROUTING_REVISION
            -- AND t.ALTERNATIVE_NO = r.ALTERNATIVE_NO
            -- AND t.BOM_TYPE_DB = h.BOM_TYPE_DB 
            -- AND t.OPERATION_ID = r.OPERATION_ID
            --
            INNER JOIN ROUTING_ALTERNATE a
            ON  a.CONTRACT = h.CONTRACT
            AND a.PART_NO = h.PART_NO
            AND a.ROUTING_REVISION = h.ROUTING_REVISION
            AND a.BOM_TYPE_DB = h.BOM_TYPE_DB 
            AND a.ALTERNATIVE_NO = r.ALTERNATIVE_NO
            --
            WHERE 1 = 1 
            AND (
                  ( REGEXP_LIKE(r.ALTERNATIVE_NO, '^-?\d*\.?\d+([eE]-?\d+)?$') OR r.ALTERNATIVE_NO = '*' )
                  AND  
                  ( REGEXP_LIKE(r.ROUTING_REVISION, '^-?\d*\.?\d+([eE]-?\d+)?$') OR r.ROUTING_REVISION = '*')
                )
            -- AND INVENTORY_PART_API.Get_Part_Status(r.CONTRACT,r.PART_NO) IN ('A','Z') 
            AND INVENTORY_PART_API.Get_Part_Status(r.CONTRACT,r.PART_NO) IN ('A') 
            AND r.PHASE_OUT_DATE is null
            AND r.BOM_TYPE_DB = 'M'
            AND a.STATE = 'Buildable'
            -- FETCH FIRST 1000 ROWS ONLY
           "
           )
         .AsNoTracking()
         .ToListAsync();

        // 
        // 
        //  where VALIDATE_CONVERSION('29.99' AS number) = 1

        //_context.IfsRoutes.ExecuteDelete();
        await _context.IfsRoutes.BatchDeleteAsync();

        var x = new IfsSourceRoute(); 

        var ifsRoutes = new List<IfsRoute>();

        
        foreach (var ora in ifsSourceRoutes)
        {
            
            var ifsRoute = new IfsRoute
            {
                Id = Guid.NewGuid(),
                Contract = ora.Contract,
                PartNo = ora.PartNo,
                RevisionNo = ora.RevisionNo,
                AlternativeNo = ora.AlternativeNo,
                AlternativeDescription = ora.AlternativeDescription,
                OperationNo = ora.OperationNo,
                OperationDescription = ora.OperationDescription,
                WorkCenterNo = ora.WorkCenterNo,
                LaborClassNo = ora.LaborClassNo,
                MachRunFactor = ora.MachRunFactor.GetValueOrDefault(),
                LaborRunFactor = ora.LaborRunFactor.GetValueOrDefault(),
                RunTimeCode = ora.RunTimeCode,
                CrewSize = ora.CrewSize.GetValueOrDefault(),
                SetupLaborClassNo = ora.SetupLaborClassNo,
                LaborSetupTime = ora.LaborSetupTime,
                MachSetupTime = ora.MachSetupTime,
                SetupCrewSize = ora.SetupCrewSize,
                MoveTime = ora.MoveTime,
                Overlap = ora.Overlap,
                OperationId = ora.OperationId,
                ToolId = ora.ToolId,
                ToolQuantity = ora.ToolQuantity,
            };
            

            //await _context.IfsRoutes.AddAsync(ifsRoute);
            ifsRoutes.Add(ifsRoute);
        }

        await _context.BulkInsertAsync(ifsRoutes);

 

        //await _context.SaveChangesAsync();

        return;
    }


}
