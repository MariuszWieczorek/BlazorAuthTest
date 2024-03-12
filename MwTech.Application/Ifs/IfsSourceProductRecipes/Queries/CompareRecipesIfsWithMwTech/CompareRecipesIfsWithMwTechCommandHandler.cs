using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Ifs.IfsSourceProductRecipes.Queries.CompareRecipesIfsWithMwTech;

public class CompareRecipesIfsWithMwTechCommandHandler : IRequestHandler<CompareRecipesIfsWithMwTechCommand, CompareRecipesIfsWithMwTechViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IOracleDbContext _oracle;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<CompareRecipesIfsWithMwTechCommandHandler> _logger;
    private readonly IProductCsvService _productCsvService;

    public CompareRecipesIfsWithMwTechCommandHandler(IApplicationDbContext context,
        IOracleDbContext oracle,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<CompareRecipesIfsWithMwTechCommandHandler> logger,
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
    public async Task<CompareRecipesIfsWithMwTechViewModel> Handle(CompareRecipesIfsWithMwTechCommand request, CancellationToken cancellationToken)
    {

        string additionalCondition = "AND x.PartNo not like '%-TE%' AND x.PartNo not like '%-T%'";

        if (request.CompareRecipeFilter.ShowTestProducts)
        {
            additionalCondition = string.Empty;
        }

        var ifsSourceProductRecipes = _context.ComparedRecipesIfsVsMwTech
           .FromSqlRaw($@"
            SELECT *
            FROM (
            SELECT ifs.Contract
            ,ca.CategoryNumber
            ,ifs.PartNo
            ,ifs.AlternativeNo
            ,ifs.RevisionNo
            ,ifs.LineSequence
            --
            ,ifs.ComponentPart as IfsComponentPart
            ,ISNULL(mw.COMPONENT_PART,'') as MwComponentPart
            --
            ,ifs.QtyPerAssembly as IfsQtyPerAssembly
            ,ifs.PartsByWeight as IfsPartsByWeight
            ,ISNULL(mw.QTY_PER_ASSEMBLY,0) as MwQtyPerAssembly
            --
            ,ifs.ShrinkageFactor as IfsShrinkageFactor
            ,ISNULL(mw.SHRINKAGE_FACTOR,0) as MwShrinkageFactor
            --
            ,ISNULL(pr.id,0) as ProductId
            ,pr.IsActive as IsProductActive
            -- TESTS
            ,iif(ifs.ShrinkageFactor = mw.SHRINKAGE_FACTOR,1,0) as  TestShrinkageFactor
            ,iif(round(ifs.PartsByWeight,2) = round(mw.QTY_PER_ASSEMBLY,2),1,0) as  TestQtyPerAssembly
            ,iif(ifs.ComponentPart = mw.COMPONENT_PART,1,0) as  TestComponentPart
            --
            ,ifs.ConsumptionItemDb as IfsConsumptionItem
            ,mw.CONSUMPTION_ITEM as MwConsumptionItem
            --
            ,iif(mw.ALTERNATIVE is not null,1,0) as TestExistAlternativeNo
            ,iif(mw.REVISION is not null,1,0) as TestExistsRevisionNo
            --,IIF( EXISTS (select top 1 * from dbo.mwtech_bom_ifs as m where trim(m.part_no) = trim(ifs.PartNo)  and ifs.RevisionNo = m.revision and m.alternative = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  ), 1 , 0)  as TestRevAndAlt1
            --,IIF( EXISTS (select top 1 * from dbo.ProductVersions as v1 inner join dbo.Products as p1 on p1.id = v1.ProductId where trim(p1.ProductNumber) = trim(ifs.PartNo)  and ifs.RevisionNo = v1.VersionNumber and v1.AlternativeNo = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  ), 1 , 0)  as TestRevAndAlt2
            --,(select count(*) from dbo.ProductVersions as v1 inner join dbo.Products as p1 on p1.id = v1.ProductId inner join dbo.boms as b on b.SetVersionId = v1.id  where trim(p1.ProductNumber) = trim(ifs.PartNo)  and ifs.RevisionNo = v1.VersionNumber and v1.AlternativeNo = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo))   as ile
            --,IIF( EXISTS (select top 1 * from dbo.Products as p where p.ProductNumber = ifs.PartNo), 1 , 0)  as ProductExists
            --
            FROM dbo.IfsProductRecipes as ifs
            LEFT JOIN dbo.Products as pr
            ON trim(pr.ProductNumber) = trim(ifs.PartNo)
            LEFT JOIN dbo.ProductCategories as ca
            ON ca.Id = pr.ProductCategoryId

            LEFT JOIN dbo.mwtech_bom_ifs as mw
            ON 1 = 1
            AND mw.QTY_PER_ASSEMBLY > 0 -- bez ujemnych ilości
            AND mw.ALTERNATIVE = iif(ifs.AlternativeNo = '*',1,ifs.AlternativeNo)  -- bo w kursorze mw mam wartości * zamiast 1
            AND iif(ifs.RevisionNo = '*',1,ifs.RevisionNo) = mw.REVISION
            AND mw.PART_NO = ifs.PartNo
            AND mw.LINE_SEQUENCE = ifs.LineSequence
            --
            WHERE 1 = 1
            AND (ISNUMERIC(ifs.AlternativeNo)=1 OR ifs.AlternativeNo = '*')
            AND (ISNUMERIC(ifs.RevisionNo)=1 OR ifs.RevisionNo = '*')
            ) as x
            WHERE 1 = 1
            AND (( TestComponentPart + TestQtyPerAssembly + TestExistsRevisionNo + TestExistAlternativeNo < 4 ) OR isProductActive = 0)
            -- AND x.PartNo not in (select ProductNumber from dbo.IFS_black_list)
            {additionalCondition}
            AND IfsPartsByWeight > 0
           "
           )
         .AsNoTracking()
         .AsQueryable();

        
        ifsSourceProductRecipes = Filter(ifsSourceProductRecipes, request.CompareRecipeFilter);

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

        var compareRecipesIfsWithMwTechViewModel = new CompareRecipesIfsWithMwTechViewModel
        {
            ComparedRecipesIfsVsMwTech = ifsSourceProductRecipesList,
            PagingInfo = request.PagingInfo
        };

        return compareRecipesIfsWithMwTechViewModel;
    }


    public IQueryable<ComparedRecipeIfsVsMwTech> Filter(IQueryable<ComparedRecipeIfsVsMwTech> comparedRecipes, CompareRecipeFilter filter)
    {
        if (filter != null)
        {

            if (filter.ProductNumber != null)
                comparedRecipes = comparedRecipes.Where(x => x.PartNo.Contains(filter.ProductNumber));
        }

        return comparedRecipes;
    }


}
