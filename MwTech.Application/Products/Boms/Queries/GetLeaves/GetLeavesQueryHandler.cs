using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Boms.Queries.GetLeaves;

public class GetLeavesQueryHandler : IRequestHandler<GetLeavesQuery, GetLeavesViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetLeavesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<GetLeavesViewModel> Handle(GetLeavesQuery request, CancellationToken cancellationToken)
    {

        int productVersionId = request.ProductVersionId;

        if (productVersionId == 0)
        {
            productVersionId =  _context.ProductVersions
                .SingleOrDefault(x=>x.ProductId == request.ProductId && x.DefaultVersion == true).Id;
        }



        IEnumerable<BomTree> bomsTree = await _context.BomTrees
                    .FromSqlInterpolated($"select * from dbo.mwtech_bom_cte({request.ProductId},{productVersionId})")
                    .Where(x => x.HowManyParts == 0)
                    .AsNoTracking()
                    .ToListAsync();


        bomsTree = calculatePhr(bomsTree);


        var vm = new GetLeavesViewModel
        {
            BomsTree = bomsTree
        };

        return vm;
    }

    private IEnumerable<BomTree> calculatePhr(IEnumerable<BomTree> boms)
    {
        foreach (var item in boms)
        {
            item.PartRubberQty = Math.Round(item.FinalPartProductQty * (decimal)item.PartContentsOfRubber * 0.01m ,2);
        }
        return boms;
    }
}
