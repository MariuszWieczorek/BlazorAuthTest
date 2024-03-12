using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Boms.Queries.GetBomTreeView;

public class GetBomTreeViewQueryHandler : IRequestHandler<GetBomTreeViewQuery, GetBomTreeViewViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetBomTreeViewQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<GetBomTreeViewViewModel> Handle(GetBomTreeViewQuery request, CancellationToken cancellationToken)
    {

        int? productVersionId = request.ProductVersionId;

        if (productVersionId == 0)
        {
            productVersionId =  _context.ProductVersions
                .SingleOrDefault(x=>x.ProductId == request.ProductId && x.DefaultVersion == true)?.Id;
        }



        IEnumerable<BomTree> bomsTree = await _context.BomTrees
                    .FromSqlInterpolated($"select * from dbo.mwtech_bom_cte({request.ProductId},{productVersionId})")
                    .AsNoTracking()
                    .ToListAsync();
        

        var topElement = bomsTree.FirstOrDefault();


        string style1 = "style = \"color: crimeson; font-weight:bold; \"";

        string htmlCode = $"<li> <div {style1}>" + topElement?.SetProductNumber + " </div></li>";
        string result = "";

        getx(topElement?.SetGrp, bomsTree, -1, ref result);

        htmlCode = htmlCode + result;


        var vm = new GetBomTreeViewViewModel
        {
            BomsTree = bomsTree,
            HtmlCode = htmlCode
        };

        return vm;
    }

    private void getx(string? index, IEnumerable<BomTree> boms, int? level, ref string result)
    {
        if (index == null)
        {
            return;
        }

        var nextLevelComponentList = boms
            .Where(x => x.SetGrp == index && x.Level == level + 1)
            .GroupBy(x => new { x.SetGrp, x.PartGrp, x.SetProductNumber, x.PartProductNumber, x.Level, x.PartProductQty, x.PartUnit, x.FinalPartProductQty });

        foreach (var item in nextLevelComponentList)
        {
            var hypens = string.Concat(Enumerable.Repeat('-', item.Key.Level + 1));
            var qty = $"<i style = \" color: rgb(0,0,255); \"> {item.Key.FinalPartProductQty} </i>";
            result = result + $"<li>{hypens} {item.Key.PartProductNumber}  &nbsp;&nbsp;&nbsp;&nbsp; {qty}  {item.Key.PartUnit} </li> ";
            // {item.Key.PartProductQty}  {item.Key.PartUnit}
            getx(item.Key.PartGrp, boms, item.Key.Level, ref result);
        }

    }
}
