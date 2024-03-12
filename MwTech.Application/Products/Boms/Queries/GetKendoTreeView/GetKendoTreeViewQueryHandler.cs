using DocumentFormat.OpenXml.Bibliography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Boms.Queries.GetKendoTreeView;

public class GetKendoTreeViewQueryHandler : IRequestHandler<GetKendoTreeViewQuery, KendoTreeViewViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductCostService _productCostService;

    public class StringToIntConverter
    {
        public string StringId { get; set; }
        public int NumericId { get; set; }
    }


    public GetKendoTreeViewQueryHandler(IApplicationDbContext context, IProductCostService productCostService)
    {
        _context = context;
        _productCostService = productCostService;
    }
    public async Task<KendoTreeViewViewModel> Handle(GetKendoTreeViewQuery request, CancellationToken cancellationToken)
    {

        int? productVersionId = request.ProductVersionId;

        if (productVersionId == 0)
        {
            productVersionId = _context.ProductVersions
                .SingleOrDefault(x => x.ProductId == request.ProductId && x.DefaultVersion == true)?.Id;
        }

        var product = _context.Products.SingleOrDefault(x => x.Id == request.ProductId);

        IEnumerable<BomTree> bomsTree = await _context.BomTrees
                    .FromSqlInterpolated($"select * from dbo.mwtech_bom_cte({request.ProductId},{productVersionId})")
                    .AsNoTracking()
                    .ToListAsync();


        var topElement = bomsTree.FirstOrDefault();

        // 103420&ProductVersionId=111196

        var bomsTree1 = new List<BomTree>();

        if (topElement != null)
        {
            var bomTree = new BomTree
            {
                Id = 0,
                ParentId = null,
                PartProductId = (int)topElement.SetProductId,
                PartProductNumber = topElement.SetProductNumber,
                PartProductName = product.Name,
                PartProductQty = topElement.SetProductQty,
                FinalPartProductQty = topElement.SetProductQty,
                PartUnit = topElement.SetUnit
            };

            bomsTree1.Add(bomTree);

        }



        bomsTree1.AddRange(bomsTree);

        ConvertId(bomsTree1);
        AddRoutesInfo(bomsTree1);


        await _productCostService.GetBomTreesComponentsCosts(bomsTree1);

        var vm = new KendoTreeViewViewModel
        {
            BomsTree = bomsTree1,
        };

        return vm;
    }

    private void ConvertId(List<BomTree> bomsTree1)
    {
        var grp = bomsTree1.GroupBy(x => x.PartGrp)
                    .Select(g => new StringToIntConverter() { StringId = g.Key, NumericId = 0 }).ToList();

        int i = 1;
        foreach (var item in grp)
        {
            item.NumericId = i++;
        }

        foreach (var item in bomsTree1)
        {
            var a = grp.SingleOrDefault(x => x.StringId == item.PartGrp);
            var b = grp.SingleOrDefault(x => x.StringId == item.SetGrp);
            if (a != null && item.Id != 0) { item.Id = a.NumericId; }
            if (b != null && item.ParentId != 0 && item.ParentId != null) { item.ParentId = b.NumericId; }
        }
    }


    private void AddRoutesInfo(List<BomTree> bomsTree1)
    {
        foreach (var item in bomsTree1)
        {
            string routesInfo = string.Empty;
            string parent = string.Empty;

            RouteVersion defRouteVersion = null;


            defRouteVersion = _context.RouteVersions
                .FirstOrDefault(x => x.ProductId == item.PartProductId && x.DefaultVersion && x.IsActive);

            var part = _context
                .Products
                .Include(x => x.ProductCategory)
                .SingleOrDefault(x => x.Id == item.PartProductId);


            if (part.ProductCategory.RouteSource == 0)
            {
                defRouteVersion = _context.RouteVersions
                .FirstOrDefault(x => x.ProductId == item.PartProductId && x.DefaultVersion && x.IsActive);
            }

            if (part.ProductCategory.RouteSource == 2)
            {
                if (part.Idx02 != null)
                {
                    var parentProduct = _context.Products
                    .Include(x => x.ProductCategory)
                    .AsNoTracking()
                    .SingleOrDefault(x => x.ProductNumber == part.Idx02);

                    if (parentProduct != null)
                        defRouteVersion = _context.RouteVersions
                                .Include(x => x.Product)
                                .AsNoTracking()
                                .FirstOrDefault(x => x.ProductId == parentProduct.Id
                                && x.ProductCategoryId == part.ProductCategoryId
                                && x.DefaultVersion);
                }

                parent = "\nwaz_" + part.Idx02;
            }


            if (part.ProductCategory.RouteSource == 1)
            {
                if (part.Idx01 != null)
                {
                    var parentProduct = _context.Products
                    .Include(x => x.ProductCategory)
                    .AsNoTracking()
                    .SingleOrDefault(x => x.ProductNumber == part.Idx01);

                    if (parentProduct != null)
                        defRouteVersion = _context.RouteVersions
                                .Include(x => x.Product)
                                .AsNoTracking()
                                .FirstOrDefault(x => x.ProductId == parentProduct.Id
                                && x.ProductCategoryId == part.ProductCategoryId
                                && x.DefaultVersion);
                }

                parent = "\nmatka_" + part.Idx01;
            }



            if (defRouteVersion != null)
            {
                var routes = _context.ManufactoringRoutes
                    .Include(x => x.Resource)
                    .Include(x => x.Operation)
                    .ThenInclude(x => x.Unit)
                    .Where(x => x.RouteVersionId == defRouteVersion.Id)
                    .AsNoTracking();

                foreach (var route in routes) 
                {
                    routesInfo += $" ka__{route.Resource.ResourceNumber} no__{route.OperationLabourConsumption.ToString("G29")} st__{route.Resource.Cost.ToString("G29")}  br__{route.ResourceQty.ToString("G29")}" ;
                    if (parent != null)
                        routesInfo += parent;

                }
                
                item.LabourData = routesInfo;
            }

        }
    }
}
