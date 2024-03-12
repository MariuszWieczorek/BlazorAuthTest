using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperations;

public class GetIfsWorkCenterOperationsQueryHandler : IRequestHandler<GetIfsWorkCenterOperationsQuery, IfsWorkCenterOperationsViewModel>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IProductService _productWeightService;
    private readonly IDateTimeService _dateTimeService;

    public GetIfsWorkCenterOperationsQueryHandler(IOracleDbContext oracleContext,
        IApplicationDbContext context,
        IScadaIfsDbContext scadaContext,
        IProductService productWeightService,
        IDateTimeService dateTimeService
        )
    {
        _oracleContext = oracleContext;
        _context = context;
        _scadaContext = scadaContext;
        _productWeightService = productWeightService;
        _dateTimeService = dateTimeService;
    }
    public async Task<IfsWorkCenterOperationsViewModel> Handle(GetIfsWorkCenterOperationsQuery request, CancellationToken cancellationToken)
    {


        var ifsOperations = _oracleContext.IfsWorkCenterOperations
                   .FromSqlInterpolated(
                   @$"select 
                        o.ORDER_NO as ""ORDER_NO"",
                        o.PART_NO as ""PART_NO"",
                        o.WORK_CENTER_NO as ""WORK_CENTER_NO"",
                        o.OPERATION_DESCRIPTION as ""OPERATION_DESCRIPTION"",
                        o.REVISED_QTY_DUE as ""REVISED_QTY_DUE"",
                        o.QTY_COMPLETE as ""QTY_COMPLETE"",
                        o.OP_START_DATE as ""OP_START_DATE"",
                        o.OP_FINISH_DATE as ""OP_FINISH_DATE"",
                        o.OP_ID as ""OP_ID"",
                        o.CF$_SHIFT as ""Shift""
                        from ifsapp.SO_OPER_DISPATCH_LIST_cfv o
                        where 1 = 1 
                        and ROWSTATE in ('Released','Started','Planned')
                        ")
                        .AsNoTracking()
                        .AsQueryable();

        //and to_char(OP_START_DATE,'MM.DD.YYYY') = to_char(SYSDATE,'MM.DD.YYYY') 
        //ifsapp.SO_OPER_DISPATCH_LIST_cfv
        //SHOP_ORDER_OPERATION_API.GET_REMAINING_QTY(ORDER_NO, RELEASE_NO, SEQUENCE_NO, OPERATION_NO) AS REMAINING_QTY

        var currentTime = _dateTimeService.Now;
        DateTime startDateFrom = currentTime.Date;
        DateTime startDateTo = currentTime.Date.AddDays(1).AddTicks(-1);

        string shift = null;
        var currentHour = currentTime.Hour;

        if (currentHour >= 6 && currentHour < 14)
        {
            shift = "1";
        }

        if (currentHour >= 14 && currentHour < 22)
        {
            shift = "2";
        }

        if (currentHour >= 22 || (currentHour >= 0 && currentHour < 6) )
        {
            shift = "3";
            if (currentHour >= 0 && currentHour < 6)
            {
                startDateFrom = startDateFrom.AddDays(-1);
                startDateTo = startDateTo.AddDays(-1);
            }
        }


        var filter = new IfsWorkCenterOperationsReportsFilter
        {
            WorkCenterNo = request.WorkCenterNo,
            Shift = shift,
            StartDateFrom = startDateFrom,
            StartDateTo  = startDateTo
        };


        ifsOperations = Filter(ifsOperations, filter);

        var ifsOperationsList = await ifsOperations
            .OrderBy(x => x.OP_START_DATE)
            .ThenBy(x => x.ORDER_NO)
            .ToListAsync();


        ifsOperationsList = await AddProductProperties(ifsOperationsList);

        ifsOperationsList = await AddComponentUsages(ifsOperationsList);




        var vm = new IfsWorkCenterOperationsViewModel
        {
            WorkCenterNo = request.WorkCenterNo,
            IfsWorkCenterOperations = ifsOperationsList,
            Filter = filter,
            PrintTime = currentTime

        };

        return vm;

    }

    private async Task<List<IfsWorkCenterOperation>> AddProductProperties(List<IfsWorkCenterOperation> ifsOperationsList)
    {
        foreach (var item in ifsOperationsList)
        {


            var product = _context.Products
                .SingleOrDefault(x => x.ProductNumber == item.PART_NO);




            if (product != null)
            {

                var productWeight = await _productWeightService.CalculateWeight(product.Id, 0);


                var properties = await _context.ProductProperties
                            .Include(x => x.ProductPropertiesVersion)
                            .ThenInclude(x => x.Product)
                            .Include(x => x.Property)
                            .Include(x => x.Property.Unit)
                            .Where(x => x.ProductPropertiesVersion.ProductId == product.Id && x.ProductPropertiesVersion.DefaultVersion == true)
                            .ToListAsync();

                if (properties.Any() == false)
                {
                    properties = await _context.ProductProperties
                            .Include(x => x.ProductPropertiesVersion)
                            .Include(x => x.ProductPropertiesVersion.Product)
                            .Include(x => x.Property)
                            .Include(x => x.Property.ProductCategory)
                            .Include(x => x.Property.Unit)
                            .Where(x =>
                             (x.ProductPropertiesVersion.Product.ProductNumber == product.Idx01 || x.ProductPropertiesVersion.Product.ProductNumber == product.Idx02)
                              && (x.Property.ProductCategoryId == product.ProductCategoryId
                              || _context.PropertiesProductCategoriesMaps
                              .Where(y => y.PropertyId == x.PropertyId && y.ProductCategoryId == product.ProductCategoryId).Any()
                              ) 
                              && x.ProductPropertiesVersion.DefaultVersion == true
                              && x.ProductPropertiesVersion.IsActive == true
                             )
                            .OrderBy(x => x.Property.ProductCategory.OrdinalNumber)
                            .ThenBy(x => x.Property.OrdinalNo)
                            .ThenBy(x => x.Property.PropertyNumber)
                            .ToListAsync();

                    // 
                    var x = properties.Count();
                }

                foreach (var prop in properties)
                {

                    item.Params.Add(prop);
                }


                item.ProductWeight = productWeight;
            }
        }

        return ifsOperationsList;
    }



    private async Task<List<IfsWorkCenterOperation>> AddComponentUsages(List<IfsWorkCenterOperation> ifsOperationsList)
    {
        foreach (var item in ifsOperationsList)
        {

            var componentUsages = new List<ComponentUsage>();

            var product = _context.Products
                            .SingleOrDefault(x => x.ProductNumber == item.PART_NO);


            if (product != null)
            {
                var x = await _context.Boms
                     .Include(x => x.Set)
                     .Where(x => x.PartId == product.Id && x.Set.TechCardNumber != null)
                     .GroupBy(x => new { x.Set.ProductNumber, x.Set.TechCardNumber, x.Layer, x.Set.Name })
                     .ToListAsync();


                foreach (var part in x)
                {
                    var y = new ComponentUsage
                    {
                        ProductNumber = part.Key.ProductNumber,
                        TechCardNumber = part.Key.TechCardNumber,
                        Layer = part.Key.Layer,
                        ProductName = part.Key.Name
                    };

                    item.ComponentUsages.Add(y);
                }



            }
        }


        return ifsOperationsList;
    }




    public IQueryable<IfsWorkCenterOperation> Filter(IQueryable<IfsWorkCenterOperation> operations, IfsWorkCenterOperationsReportsFilter filter)
    {
        if (filter != null)
        {

            
            if (!string.IsNullOrWhiteSpace(filter.Shift))
                operations = operations.Where(x => 
                x.Shift.ToUpper().Contains(filter.Shift.ToUpper()) || String.IsNullOrWhiteSpace(x.Shift)
                );
            

            if (!string.IsNullOrWhiteSpace(filter.WorkCenterNo))
                if (filter.WorkCenterNo.Contains("%") || filter.WorkCenterNo.Contains("_"))
                {
                    operations = operations.Where(x => EF.Functions.Like(x.WORK_CENTER_NO, filter.WorkCenterNo));
                }
                else
                {
                    operations = operations.Where(x => x.WORK_CENTER_NO.ToUpper().Contains(filter.WorkCenterNo.ToUpper()));
                }

            if (filter.StartDateFrom != null)
            {
                operations = operations.Where(x => x.OP_START_DATE >= filter.StartDateFrom);
            }

            if (filter.StartDateTo != null)
            {
                operations = operations.Where(x => x.OP_START_DATE <= filter.StartDateTo);
            }

        }

        return operations;
    }

}
