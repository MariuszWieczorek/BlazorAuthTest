using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Ifs.IfsSourceProductStructures.Queries.CompareStructuresMwTechWithIfs;

public class CompareStructuresMwTechWithIfsCommandHandler : IRequestHandler<CompareStructuresMwTechWithIfsCommand, CompareStructuresMwTechWithIfsViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IOracleDbContext _oracle;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<CompareStructuresMwTechWithIfsCommandHandler> _logger;
    private readonly IProductCsvService _productCsvService;

    public CompareStructuresMwTechWithIfsCommandHandler(IApplicationDbContext context,
        IOracleDbContext oracle,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<CompareStructuresMwTechWithIfsCommandHandler> logger,
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
    public async Task<CompareStructuresMwTechWithIfsViewModel> Handle(CompareStructuresMwTechWithIfsCommand request, CancellationToken cancellationToken)
    {

        var ifsSourceProductRecipes = _context.ComparedStructuresIfsVsMwTech
           .FromSqlRaw($@"
            SELECT *
            FROM (
            SELECT 'KTx' Contract
            ,ca.CategoryNumber
            ,mw.PART_NO as PartNo
            ,CAST( mw.ALTERNATIVE_NO as varchar(10))  as AlternativeNo
            ,CAST( mw.REVISION as varchar(10))  as RevisionNo
            ,mw.LINE_SEQUENCE as LineSequence
            --
            ,ISNULL(ifs.ComponentPart,'') as IfsComponentPart
            ,ISNULL(mw.COMPONENT_PART,'') as MwComponentPart
            --
            ,ISNULL(ifs.QtyPerAssembly,0) as IfsQtyPerAssembly
            ,ISNULL(mw.QTY_PER_ASSEMBLY,0) as MwQtyPerAssembly
            --
            ,ISNULL(ifs.ShrinkageFactor,0) as IfsShrinkageFactor
            ,ISNULL(mw.SHRINKAGE_FACTOR,0) as MwShrinkageFactor
            --
            ,ISNULL(pr.id,0) as ProductId
            ,pr.IsActive as IsProductActive
            -- TESTS
            ,iif(ifs.ShrinkageFactor = mw.SHRINKAGE_FACTOR,1,0) as  TestShrinkageFactor
            ,iif(round(ifs.QtyPerAssembly,2) = round(mw.QTY_PER_ASSEMBLY,2),1,0) as  TestQtyPerAssembly
            ,iif(ifs.ComponentPart = mw.COMPONENT_PART,1,0) as  TestComponentPart
            --
            ,ifs.ConsumptionItemDb as IfsConsumptionItem
            ,mw.CONSUMPTION_ITEM as MwConsumptionItem
            --
            ,1 as TestProductExists
            ,1 as TestRevAndAltExists
            ,iif(mw.ALTERNATIVE is not null,1,0) as TestExistAlternativeNo
            ,iif(mw.REVISION is not null,1,0) as TestExistsRevisionNo
            --
            FROM dbo.mwtech_bom_ifs as mw 
            LEFT JOIN dbo.Products as pr
            ON pr.Id = mw.SetId
            LEFT JOIN dbo.ProductCategories as ca
            ON ca.Id = pr.ProductCategoryId
            --
            LEFT JOIN dbo.IfsProductStructures as ifs 
            ON 1 = 1
            AND mw.ALTERNATIVE = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  -- bo w kursorze mw mam wartości * zamiast 1
            AND iif(ifs.RevisionNo = '*',1,ifs.RevisionNo) = mw.REVISION
            AND mw.PART_NO = ifs.PartNo
            AND mw.LINE_SEQUENCE = ifs.LineSequence
            --
            WHERE 1 = 1
            AND ca.CategoryNumber NOT IN ('NAW','MOD','MON','MOP')
            AND ca.CategoryNumber NOT LIKE 'MIE%'
            --
            AND pr.IsTest = 0  -- bez testowych
            AND pr.IsActive = 1 -- tylko aktywne
            AND pr.ProductNumber not like 'KO%G2' -- odrzucamy drugie gatunki
            AND pr.ProductNumber not like 'MEM-%' -- odrzucamy membrany
            AND pr.ProductNumber not like '%-TE' -- odrzucamy testowe
            AND mw.QTY_PER_ASSEMBLY > 0 -- bez ujemnych ilości
            --
            ) as x
            WHERE 1 = 1
            AND (( TestComponentPart + TestQtyPerAssembly + TestExistsRevisionNo + TestExistAlternativeNo < 4 ) OR isProductActive = 0)     
           "
           )
         .AsNoTracking()
         .AsQueryable();

        
        ifsSourceProductRecipes = Filter(ifsSourceProductRecipes, request.CompareStructureFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            
            request.PagingInfo.ItemsPerPage = 100;
            /*
            if (request.PagingInfo.ItemsPerPage > 0 )
                ifsSourceProductRecipes = ifsSourceProductRecipes
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
            */
        }

        var ifsSourceProductRecipesList = await ifsSourceProductRecipes
            .OrderBy(x => x.PartNo)
            .ThenBy(x => x.RevisionNo)
            .ThenBy(x => x.AlternativeNo)
            .ThenBy(x => x.LineSequence)
            .ToListAsync();

        request.PagingInfo.TotalItems = ifsSourceProductRecipesList.Count();

        var compareStructuresMwTechWithIfsViewModel = new CompareStructuresMwTechWithIfsViewModel
        {
            ComparedStructuresIfsVsMwTech = ifsSourceProductRecipesList,
            PagingInfo = request.PagingInfo
        };

        return compareStructuresMwTechWithIfsViewModel;
    }


    public IQueryable<ComparedStructureIfsVsMwTech> Filter(IQueryable<ComparedStructureIfsVsMwTech> comparedStructures, CompareStructureFilter filter)
    {
        if (filter != null)
        {

            if (filter.ProductNumber != null)
                comparedStructures = comparedStructures.Where(x => x.PartNo.Contains(filter.ProductNumber));
        }

        return comparedStructures;
    }


}
