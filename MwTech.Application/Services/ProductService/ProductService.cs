using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<ProductService> logger
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
    }

    private async Task<List<Bom>> GetSimpleBoms(int productId, int productVersionId)
    {
        var boms = await _context.Boms
                        .Include(x => x.Part)
                        .ThenInclude(x => x.Unit)
                        .Where(x => x.SetVersionId == productVersionId && x.SetId == productId)
                        .AsNoTracking()
                        .ToListAsync();

        boms = CalculateWeight(boms);
        boms = CalculatePhr(boms);

        return boms;
    }
    public async Task<decimal> CalculateWeight(int productId, int productVersionId = 0)
    {
        decimal calculatedWeight = 0;
        var productVersion = new ProductVersion();


        if (productVersionId == 0)
            productVersion = _context.ProductVersions.AsNoTracking().FirstOrDefault(x => x.ProductId == productId && x.DefaultVersion);
        else
            productVersion = _context.ProductVersions.AsNoTracking().FirstOrDefault(x => x.ProductId == productId && x.Id == productVersionId);


        if (productVersion != null)
            productVersionId = productVersion.Id;
        else
            return 0M;

        var boms = await GetSimpleBoms(productId, productVersionId);

        calculatedWeight = Math.Round(boms.Where(x =>  !x.DoNotIncludeInWeight).Sum(x => x.PartWeight), 6);
        // x.OnProductionOrder &&

        if (calculatedWeight == 0)
        {
            calculatedWeight = productVersion.ProductWeight;
        }

        return calculatedWeight;
    }
    public List<Bom> CalculateWeight(List<Bom> boms)
    {
        foreach (var item in boms)
        {
            // metoda zwróci sumaryczną ilość komponentów
            // potrzebną do wyprodukowania określonej ilości produktu: setProductQty
            // Dlatego gdy receptura jest na ilość produktu różną od 1
            // Musimy przez tą wartość podzielić, aby uzyskać wagę jednostkową



            var bomsTree = new List<BomTree>();

            try
            {
                // tylko komponenty, które nie mają już składu
                bomsTree = _context.BomTrees
                .FromSqlInterpolated($"select * from dbo.mwtech_bom_cte({item.PartId},default)")
                .Where(x => x.HowManyParts == 0)
                .AsNoTracking()
                .ToList();
            }
            catch (Exception)
            {
                // na wypadek wystąpienia zapętlenia w bomie

            }


            var weight = 0M;

            if (bomsTree.Count() > 0) // jeżeli jest jakiekolwiek drzewo 
            {
                // waga jednostkowa składnika wyliczona w wyrażeniu CTE mwtech_bom_cte
                //var unitWeight = bomsTree.Where(x=>x.PartDoesNotIncludeInWeight==false).Sum(x => x.FinalPartProductWeight);
                var unitWeight = bomsTree
                    .Where(x => !x.PartDoesNotIncludeInWeight)
                    .Sum(x => x.FinalPartProductWeight);

                // x.PartOnProductionOrder &&

                unitWeight = Math.Round(unitWeight, 5);

                // ilość Produktu w domyślnej wersji receptury na składnik
                var setProductQty = _context.ProductVersions.Single(x => x.ProductId == item.PartId && x.DefaultVersion).ProductQty;

                // waga jednostkowa
                if (setProductQty != 0)
                    unitWeight = unitWeight / setProductQty;

                // waga w bom
                // weight = unitWeight * item.PartQty;
                weight = Math.Round(unitWeight * item.PartQty, 6);

            }
            else
            {
                if (item.Part.Unit.Weight)
                {
                    // weight = item.PartQty;
                    weight = Math.Round(item.PartQty, 6);
                }
                else
                {
                    var product = _context.ProductVersions
                        .FirstOrDefault(x => x.ProductId == item.Part.Id && x.DefaultVersion);
                    var productWeight = product == null ? 0M : product.ProductWeight;
                    //weight = productWeight * item.PartQty;
                    weight = Math.Round(productWeight * item.PartQty, 6);
                }
            }

            item.PartWeight = weight;
            // _context.Clear();
        }

        return boms;
    }



    public List<Bom> CalculatePhr(List<Bom> boms)
    {
        foreach (var item in boms)
        {

            var totalContentsOfRubber = boms
                .Where(x => x.Part.ContentsOfRubber != 0)
                .Sum(x => x.PartWeight * x.Part.ContentsOfRubber * 0.01m);

            var totalWeight = boms.Sum(x => x.PartWeight);

            if (totalContentsOfRubber != 0)
            {
                item.Phr = Math.Round(item.PartWeight / totalContentsOfRubber * 100, 2);
            }

        }

        return boms;
    }

    public async Task<decimal> CalculatePhr(int productId, int productVersionId = 0)
    {
        decimal calculatedPhr = 0;

        if (productVersionId == 0)
        {
            var productVersion = _context.ProductVersions.SingleOrDefault(x => x.ProductId == productId && x.DefaultVersion);
            if (productVersion != null)
            {
                productVersionId = productVersion.Id;
            }
        }

        if (productVersionId == 0)
        {
            return 0M;
        }

        var boms = await GetSimpleBoms(productId, productVersionId);

        // boms = CalculateWeight(boms);
        calculatedPhr = boms.Sum(x => x.Phr);
        return calculatedPhr;
    }

    public async Task<List<ComponentUsage>> GetComponentUsages(string productNumber)
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
