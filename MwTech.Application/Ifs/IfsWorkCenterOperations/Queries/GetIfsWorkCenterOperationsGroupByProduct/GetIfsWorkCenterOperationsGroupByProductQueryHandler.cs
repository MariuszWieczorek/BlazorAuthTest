using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Ifs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Ifs.IfsWorkCenterOperations.Queries.GetIfsWorkCenterOperationsGroupByProduct;

public class GetIfsWorkCenterOperationsGroupByProductQueryHandler : IRequestHandler<GetIfsWorkCenterOperationsGroupByProductQuery, IfsWorkCenterOperationsGroupByProductViewModel>
{
    private readonly IOracleDbContext _oracleContext;
    private readonly IApplicationDbContext _context;
    private readonly IScadaIfsDbContext _scadaContext;
    private readonly IProductService _productService;

    public GetIfsWorkCenterOperationsGroupByProductQueryHandler(IOracleDbContext oracleContext, IApplicationDbContext context, IScadaIfsDbContext scadaContext, IProductService productService)
    {
        _oracleContext = oracleContext;
        _context = context;
        _scadaContext = scadaContext;
        _productService = productService;
    }
    public async Task<IfsWorkCenterOperationsGroupByProductViewModel> Handle(GetIfsWorkCenterOperationsGroupByProductQuery request, CancellationToken cancellationToken)
    {



        var ifsOperations = _oracleContext.IfsWorkCenterOperations
                   .FromSqlInterpolated(
                   @$"select 
                        o.ORDER_NO,
                        o.PART_NO,
                        o.WORK_CENTER_NO,
                        o.OPERATION_DESCRIPTION,
                        o.REVISED_QTY_DUE,
                        o.QTY_COMPLETE,
                        o.OP_START_DATE,
                        o.OP_FINISH_DATE,
                        o.OP_ID,
                        o.CF$_SHIFT as ""Shift""
                        from ifsapp.SO_OPER_DISPATCH_LIST_cfv o
                        where 1 = 1 
                        and ROWSTATE in ('Released','Started','Planned')
                        and (
                        to_char(OP_START_DATE,'MM.DD.YYYY') = to_char(SYSDATE,'MM.DD.YYYY')
                        or
                        to_char(OP_START_DATE,'MM.DD.YYYY') = to_char(SYSDATE-1,'MM.DD.YYYY')
                        or
                        to_char(OP_START_DATE,'MM.DD.YYYY') = to_char(SYSDATE+1,'MM.DD.YYYY')
                        )
                        ")
                        .AsNoTracking()
                        .AsQueryable();

        // dodałem do Where dzień do tyłu


        var allOperationsForWorkCenterList =
            await ifsOperations
            .Where(x => x.WORK_CENTER_NO == request.WorkCenterNo)
            .OrderBy(x => x.ORDER_NO)
            .ToListAsync();


        var allOperationsListGroupByProduct =
            allOperationsForWorkCenterList
            .GroupBy(x => new { x.PART_NO })
            .ToList();


        var ppp = new List<IfsWorkCenterOperationsGroupByProduct>();

        foreach (var product in allOperationsListGroupByProduct)
        {



            var y = allOperationsForWorkCenterList
                .Where(x => x.PART_NO == product.Key.PART_NO)
                .ToList();

            var x = new IfsWorkCenterOperationsGroupByProduct
            {
                ProductNumber = product.Key.PART_NO,
                IfsWorkCenterOperations = y,
                ComponentUsages = await GetComponentUsages(product.Key.PART_NO)

            };

            ppp.Add(x);

        }


        // allOperationsForWorkCenterList = await AddParams(allOperationsForWorkCenterList);






        var vm = new IfsWorkCenterOperationsGroupByProductViewModel
        {
            WorkCenterNo = request.WorkCenterNo,
            IfsWorkCenterOperationsGroupByProduct = ppp,

        };

        return vm;

    }

    private async Task<List<IfsWorkCenterOperation>> AddParams(List<IfsWorkCenterOperation> ifsOperationsList)
    {
        foreach (var item in ifsOperationsList)
        {


            var product = _context.Products
                .SingleOrDefault(x => x.ProductNumber == item.PART_NO);


            if (product != null)
            {


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



            }
        }

        return ifsOperationsList;
    }



    private async Task<List<ComponentUsage>> GetComponentUsages(string productNumber)
    {


        var componentUsages = new List<ComponentUsage>();

        var product = _context.Products
                        .SingleOrDefault(x => x.ProductNumber == productNumber);


        if (product != null)
        {
            int productId = product.Id;

            var x = await _context.Boms
                 .Include(x => x.Set)
                 .Where(x => x.PartId == productId && x.Set.TechCardNumber != null)
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

                componentUsages.Add(y);
            }



        }


        return componentUsages;
    }


}
