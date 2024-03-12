using EFCore.BulkExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Ifs;
using MwTech.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Ifs.IfsSourceProductRecipes.Command.ImportIfsSourceProductRecipes;

public class ImportIfsSourceProductRecipesCommandHandler : IRequestHandler<ImportIfsSourceProductRecipesCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IOracleDbContext _oracle;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<ImportIfsSourceProductRecipesCommandHandler> _logger;
    private readonly IProductCsvService _productCsvService;

    public ImportIfsSourceProductRecipesCommandHandler(IApplicationDbContext context,
        IOracleDbContext oracle,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<ImportIfsSourceProductRecipesCommandHandler> logger,
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
    public async Task Handle(ImportIfsSourceProductRecipesCommand request, CancellationToken cancellationToken)
    {

        var ifsSourceProductRecipes = await _oracle.IfsSourceProductRecipes
           .FromSqlRaw($@"SELECT
            h.CONTRACT
            , h.PART_NO
            -- , INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) as PART_STATUS
            -- , INVENTORY_PART_API.Get_Description(s.Contract,s.PART_NO) as PART_NAME
            , h.ENG_CHG_LEVEL as REVISION_NO
            , PART_REVISION_API.Get_Revision_Text(h.contract,h.part_no,h.ENG_CHG_LEVEL) as REVISION_NAME
            , v.ALTERNATIVE_NO
            , v.ALTERNATIVE_DESCRIPTION
            , MANUF_STRUCT_ALTERNATE_API.Get_State(h.contract,h.part_no,h.eng_chg_level,h.bom_type,v.alternative_no) as ALTERNATIVE_STATE
            , s.LINE_ITEM_NO
            , s.LINE_SEQUENCE
            , s.COMPONENT_PART
            , s.QTY_PER_ASSEMBLY
            , s.PARTS_BY_WEIGHT
            , s.CONSUMPTION_ITEM_DB
            , s.PRINT_UNIT
            , s.EFF_PHASE_IN_DATE
            , s.EFF_PHASE_OUT_DATE
            , s.COMPONENT_SCRAP
            , s.SHRINKAGE_FACTOR
            , INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) as PART_STATUS
            --
            FROM RECIPE_STRUCTURE_HEAD  h
            INNER JOIN RECIPE_STRUCT_ALTERNATE v
            on  v.part_no = h.part_no 
            and v.eng_chg_level = h.eng_chg_level
            and v.contract = h.contract
            and v.state = 'Buildable'        
            --
            INNER JOIN RECIPE_STRUCTURE s
            on  s.part_no = v.part_no 
            and s.alternative_no = v.alternative_no
            and s.eng_chg_level = v.eng_chg_level
            and s.contract = v.contract
            --
            where 1 = 1
            and ( s.EFF_PHASE_OUT_DATE is null OR s.EFF_PHASE_OUT_DATE  > CURRENT_DATE  )
            and INVENTORY_PART_API.Get_Part_Status(h.CONTRACT,h.PART_NO) = 'A'
            and v.alternative_no != '1'
           "
           )
         .AsNoTracking()
         .ToListAsync();

        await _context.IfsProductRecipes.BatchDeleteAsync();

        var ifsProductRecipes = new List<IfsProductRecipe>();

        foreach (var ora in ifsSourceProductRecipes)
        {
            var ifsProductRecipe = new IfsProductRecipe
            {
                Id = Guid.NewGuid(),
                Contract = ora.CONTRACT,
                PartNo = ora.PART_NO,
                RevisionNo = ora.REVISION_NO,
                AlternativeNo = ora.ALTERNATIVE_NO,
                LineSequence = ora.LINE_SEQUENCE,
                LineItemNo = ora.LINE_ITEM_NO,
                RevisionName = ora.REVISION_NAME,
                AlternativeDescription = ora.ALTERNATIVE_DESCRIPTION,
                AlternativeState = ora.ALTERNATIVE_STATE,
                ComponentPart = ora.COMPONENT_PART,
                QtyPerAssembly = ora.QTY_PER_ASSEMBLY,
                PartsByWeight = ora.PARTS_BY_WEIGHT,
                ShrinkageFactor = ora.SHRINKAGE_FACTOR,
                ComponentScrap = ora.COMPONENT_SCRAP,
                ConsumptionItemDb = ora.CONSUMPTION_ITEM_DB,
                PrintUnit = ora.PRINT_UNIT,
                EffPhaseInDate = ora.EFF_PHASE_IN_DATE,
                EffPhaseOutDate = ora.EFF_PHASE_OUT_DATE,
                PartStatus = ora.PART_STATUS
            };

            ifsProductRecipes.Add(ifsProductRecipe);
        }

        await _context.BulkInsertAsync(ifsProductRecipes);

        return;
    }


}
