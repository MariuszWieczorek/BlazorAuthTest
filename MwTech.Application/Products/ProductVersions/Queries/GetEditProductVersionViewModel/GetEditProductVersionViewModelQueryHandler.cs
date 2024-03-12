using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Application.Products.ProductVersions.Commands.EditProductVersion;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductVersions.Queries.GetEditProductVersionViewModel;

public class GetEditProductVersionViewModelQueryHandler : IRequestHandler<GetEditProductVersionViewModelQuery, EditProductVersionViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;
    private readonly IProductCostService _productCostService;

    public GetEditProductVersionViewModelQueryHandler(
        IApplicationDbContext context,
        IProductService productWeightService,
        IProductCostService productCostService
        )
    {
        _context = context;
        _productWeightService = productWeightService;
        _productCostService = productCostService;
    }

    public async Task<EditProductVersionViewModel> Handle(GetEditProductVersionViewModelQuery request, CancellationToken cancellationToken)
    {

        if (request.ProductVersionId == 0)
        {
            request.ProductVersionId = _context.ProductVersions.FirstOrDefault(x=>x.ProductId == request.ProductId && x.DefaultVersion).Id;
        }


        var productVersion = await _context.ProductVersions
            .FirstOrDefaultAsync(x => x.Id == request.ProductVersionId && x.ProductId == request.ProductId);

        var productVersions = await _context.ProductVersions
            .Include(x => x.Accepted01ByUser)
            .Include(x => x.Accepted02ByUser)
            .Include(x => x.CreatedByUser)
            .Where(x => x.ProductId == request.ProductId)
            .OrderByDescending(x => x.VersionNumber)
            .AsNoTracking()
            .ToListAsync();

        var boms = await GetBoms(request);
        
        var bomLeaves = await GetBomLeaves(request);

        var editProductVersionCommand = new EditProductVersionCommand
        {
            Id = request.ProductVersionId,
            ProductId = request.ProductId,
            Product = await _context.Products.SingleOrDefaultAsync(x=>x.Id == request.ProductId),
            VersionNumber = productVersion.VersionNumber,
            AlternativeNo = productVersion.AlternativeNo,
            Name = productVersion.Name,
            Description = productVersion.Description,
            ProductQty = productVersion.ProductQty,
            ProductWeight = productVersion.ProductWeight,
            IsActive = productVersion.IsActive,
            ToIfs = productVersion.ToIfs,
            ModifiedByUserId = productVersion.ModifiedByUserId,
            ModifiedDate = productVersion.ModifiedDate,
            IsAccepted01 = productVersion.IsAccepted01,
            IsAccepted02 = productVersion.IsAccepted02,
            IfsDefaultVersion = productVersion.IfsDefaultVersion,
            ComarchDefaultVersion = productVersion.ComarchDefaultVersion,
        };


        var vm = new EditProductVersionViewModel
        {
            EditProductVersionCommand = editProductVersionCommand,
            Boms = boms,
            BomLeaves = bomLeaves
        };
        
        return vm;
    }

    private async Task<List<Bom>> GetBoms(GetEditProductVersionViewModelQuery request)
    {
        var boms = await _context.Boms
                        .Include(x => x.Set)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.Set)
                        .ThenInclude(x => x.ProductCategory)
                        .Include(x => x.Part)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.Part)
                        .ThenInclude(x => x.ProductCategory)
                        .Where(x => x.SetVersionId == request.ProductVersionId && x.SetId == request.ProductId)
                        .OrderBy(x => x.OrdinalNumber)
                        .ThenBy(x => x.Part.ProductNumber)
                        .AsNoTracking()
                        .ToListAsync();

         boms = _productWeightService.CalculateWeight(boms);
         boms = _productWeightService.CalculatePhr(boms);
         boms = await _productCostService.GetBomComponentsCosts(boms);

        return boms;
    }


    private async Task<List<BomTree>> GetBomLeaves(GetEditProductVersionViewModelQuery request)
    {
        
        List<BomTree> bomsTree = await _context.BomTrees
                  .FromSqlInterpolated($"select * from dbo.mwtech_bom_cte({request.ProductId},{request.ProductVersionId})")
                  .Where(x => x.HowManyParts == 0)
                  .AsNoTracking()
                  .ToListAsync();


        bomsTree = CalculatePhr(bomsTree);
        bomsTree = CalculateVolume(bomsTree);
        bomsTree = await GetPrices(bomsTree);


        return bomsTree;
    }

    private List<BomTree> CalculatePhr(List<BomTree> boms)
    {
        foreach (var item in boms)
        {
            item.PartRubberQty = Math.Round( Math.Round(item.FinalPartProductQty,2) * (decimal)item.PartContentsOfRubber * 0.01m, 2);
        }
        var totalRubberQty = Math.Round((decimal)boms.Where(x=>x.PartOnProductionOrder).Sum(x => x.PartRubberQty),2);
        
        foreach (var item in boms)
        {
            if (totalRubberQty != 0 && item.PartOnProductionOrder)
            {
                item.PartPhr = Math.Round( (item.FinalPartProductQty / (decimal)totalRubberQty) * 100, 2);
            }
        }

        return boms;
    }


    private List<BomTree> CalculateVolume(List<BomTree> boms)
    {
        foreach (var item in boms)
        {
            if ((decimal)item.PartDensity != 0 && item.PartOnProductionOrder)
            {
                item.PartVolume = Math.Round(Math.Round(item.FinalPartProductQty, 2) / (decimal)item.PartDensity , 2);
            }
            else
            {
                item.PartVolume = 0;
            }
            
        }
        var totalVolume = Math.Round((decimal)boms.Where(x => x.PartOnProductionOrder).Sum(x => x.PartVolume), 2);

        return boms;
    }


    private async Task<List<BomTree>> GetPrices(List<BomTree> boms)
    {
        foreach (var item in boms)
        {
            item.PartMaterialPrice = await _productCostService.GetProductPrice(item.PartProductId);
            item.PartMaterialCost = (decimal)Math.Round((decimal)item.PartMaterialPrice * item.FinalPartProductQty,2);
            var x = item.PartMaterialCost;

        }
        

        return boms;
    }
}




