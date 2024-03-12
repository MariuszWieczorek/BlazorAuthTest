using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.Services.ProductCostService;

public class EstimatedProductCostService : IEstimatedProductCostService
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<ProductCostService> _logger;

    private readonly AccountingPeriod _defaultPeriod;

    public EstimatedProductCostService(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<ProductCostService> logger
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _defaultPeriod = _context.AccountingPeriods.SingleOrDefault(x => x.IsDefault);
    }

    public async Task CaluclateEstimatedProductCost(int productId)
    {

        var userId = _currentUser.UserId;

        var product = _context.Products.SingleOrDefault(x => x.Id == productId);

        if (product.NoCalculateTkw)
            return;


        var defaultPeriod = _context.AccountingPeriods.SingleOrDefault(x => x.IsDefault == true);

        if (defaultPeriod == null)
        {
            _logger.LogWarning("Kalkulacja kosztów: Brak domyślnego okresu !");
            throw new Exception("Kalkulacja kosztów: Brak domyślnego okresu !");
        }

        var defaultProductVersion = _context.ProductVersions
            .SingleOrDefault(x => x.ProductId == productId && x.DefaultVersion);

        if (defaultProductVersion == null)
        {
            _logger.LogWarning($"Kalkulacja kosztów: ProductId = {productId} brak domyślnej wersji struktury !");
            throw new Exception($"Kalkulacja kosztów: ProductId = {productId} brak domyślnej wersji struktury !");
        }

        var defaultRouteVersion = _context.RouteVersions
            .SingleOrDefault(x => x.ProductId == productId && x.DefaultVersion);


        if (defaultRouteVersion == null)
        {
            _logger.LogWarning($"Kalkulacja kosztów: brak domyślnej wersji marszruty {productId}");
            throw new Exception($"Kalkulacja kosztów: brak domyślnej wersji marszruty {productId} !");
        }



        // koszty materiałowe 
        var boms = await _context.Boms
                         .Include(x => x.Set)
                         .ThenInclude(x => x.Unit)
                         .Include(x => x.Set)
                         .ThenInclude(x => x.ProductCategory)
                         .Include(x => x.Part)
                         .ThenInclude(x => x.Unit)
                         .Include(x => x.Part)
                         .ThenInclude(x => x.ProductCategory)
                         .Where(x => x.SetId == productId && x.SetVersionId == defaultProductVersion.Id)
                         .OrderBy(x => x.OrdinalNumber)
                         .ThenBy(x => x.Part.ProductNumber)
                         .AsNoTracking()
                         .ToListAsync();

        // dla każdego komponentu przypisujemy koszt
        boms = await GetEstimatedBomComponentsCosts(boms);

        // sumujemy oddzielnie każdy rodzaj kosztu
        decimal bomsTotalCost = boms.Sum(x => x.TotalCost);
        decimal bomsTotalLabourCost = boms.Sum(x => x.TotalLabourCost);
        decimal bomsTotalMaterialCost = boms.Sum(x => x.TotalMaterialCost);
        decimal bomsTotalMarkupCost = boms.Sum(x => x.TotalMarkupCost);




        // koszty robocizny
        var routes = await _context.ManufactoringRoutes
                .Include(x => x.RouteVersion)
                .ThenInclude(x => x.Product)
                .Include(x => x.Operation)
                .ThenInclude(x => x.Unit)
                .Include(x => x.Resource)
                .ThenInclude(x => x.Unit)
                .Include(x => x.WorkCenter)
                .Where(y => y.RouteVersionId == defaultRouteVersion.Id)
                .AsNoTracking()
                .ToListAsync();


        decimal routeTotalCost = await GetTotalRouteCost(defaultPeriod, routes);


        // koszt jednostkowy

        decimal totalQty = defaultProductVersion.ProductQty;

        if (totalQty == 0)
            totalQty = 1;


        decimal unitMaterialCost = bomsTotalMaterialCost / totalQty;
        decimal unitLabourCost = (routeTotalCost + bomsTotalLabourCost) / totalQty;
        decimal unitProductLabourCost = routeTotalCost / totalQty;

        decimal unitTotalCost = (bomsTotalCost + routeTotalCost) / totalQty;


        var productCostToUpdate = _context.ProductCosts
            .SingleOrDefault(x => x.ProductId == productId && x.AccountingPeriodId == defaultPeriod.Id);

        bool newProductCost = productCostToUpdate == null;

        if (newProductCost)
        {
            productCostToUpdate = new ProductCost
            {
                ProductId = productId,
                AccountingPeriodId = defaultPeriod.Id,
                CreatedByUserId = userId
            };

        }

        productCostToUpdate.Cost = unitTotalCost;
        productCostToUpdate.MaterialCost = unitMaterialCost;
        productCostToUpdate.LabourCost = unitLabourCost;
        productCostToUpdate.ProductLabourCost = unitProductLabourCost;

        productCostToUpdate.CurrencyId = 1;
        productCostToUpdate.IsCalculated = true;
        productCostToUpdate.CalculatedDate = _dateTimeService.Now;




        if (newProductCost)
        {
            productCostToUpdate.CreatedByUserId = userId;
            productCostToUpdate.CreatedDate = _dateTimeService.Now;
            await _context.ProductCosts.AddAsync(productCostToUpdate);
        }
        else
        {
            productCostToUpdate.ModifiedByUserId = userId;
            productCostToUpdate.ModifiedDate = _dateTimeService.Now;
            _context.ProductCosts.Update(productCostToUpdate);
        }

        await _context.SaveChangesAsync();

    }


    #region RouteCost

    private async Task<decimal> GetTotalRouteCost(AccountingPeriod defaultPeriod, List<ManufactoringRoute> routes)
    {

        routes = await GetEstimatedRoutesCost(routes);
        decimal totalRouteCost = routes.Sum(x => x.TotalCost);
        return totalRouteCost;
    }



    public async Task<List<ManufactoringRoute>> GetEstimatedRoutesCost(List<ManufactoringRoute> routes)
    {
        foreach (var item in routes)
        {
            var resourceCost = item?.Resource?.Cost ?? 0m;

            var workCenterCost = item?.WorkCenter?.Cost ?? 0m;


            decimal labourMarkup = item.Resource.Markup * 0.01m;
            decimal labourCostWithoutMarkup = 0m;
            decimal totalLabourCost;

            decimal workCenterMarkup = item.WorkCenter.Markup * 0.01m;
            decimal workCenterCostWithoutMarkup = 0m;
            decimal totalWorkCenterCost;

            var resourceUnitCode = item?.Resource?.Unit?.UnitCode;

            // czas w części godziny na jm lub wydajność jm na godzinę
            var operationUnitCode = item?.Operation?.Unit?.UnitCode;


            if (operationUnitCode == "godz/szt"
               || operationUnitCode == "godz/kg"
               || operationUnitCode == "godz/m"
               || operationUnitCode == "Godz./jedn.")
            {
                labourCostWithoutMarkup = Math.Round(item.ResourceQty * resourceCost * item.OperationLabourConsumption, 2);
                workCenterCostWithoutMarkup = Math.Round(workCenterCost * item.OperationMachineConsumption, 2);
            }
            else
            {
                if (item.OperationLabourConsumption != 0)
                {
                    labourCostWithoutMarkup = Math.Round(item.ResourceQty * resourceCost / item.OperationLabourConsumption, 2);
                }

                if (item.OperationMachineConsumption != 0)
                {
                    workCenterCostWithoutMarkup = Math.Round(workCenterCost / item.OperationMachineConsumption, 2);
                }

            }

            // wyjątek dla mieszanek
            if (resourceUnitCode == "pln/sec")
                labourCostWithoutMarkup = Math.Round(item.ResourceQty * resourceCost * item.OperationLabourConsumption, 2);



            totalLabourCost = Math.Round(labourCostWithoutMarkup * (1 + labourMarkup), 2);
            totalWorkCenterCost = Math.Round(workCenterCostWithoutMarkup * (1 + workCenterMarkup), 2);



            item.CostWithoutMarkup = labourCostWithoutMarkup + workCenterCostWithoutMarkup;
            item.TotalCost = totalLabourCost + totalWorkCenterCost;

        }


        return routes;
    }


    #endregion

    public async Task<List<Product>> GetEstimatedProductsCost(List<Product> products)
    {
        var defaultPeriod = _context.AccountingPeriods.SingleOrDefault(x => x.IsDefault);

        foreach (var item in products)
        {
            var productCost = _context.ProductCosts
                .Include(x => x.Currency)
                .SingleOrDefault(x => x.ProductId == item.Id && x.AccountingPeriodId == defaultPeriod.Id);

            if (productCost != null)
            {
                item.Cost = CalcutateToPln(productCost.EstimatedCost, productCost.Currency, defaultPeriod.Id);
                item.LabourCost = productCost.EstimatedLabourCost;
                item.MaterialCost = productCost.EstimatedMaterialCost;
                item.ProductLabourCost = productCost.EstimatedProductLabourCost;
            }

        }

        return products;
    }

    /// <summary>
    /// Pobiera wyliczony wcześniej koszt w ProductCost
    /// dla każdego komponentu struktury produktowej 
    /// Osobno Robociznę, Materiały i Całkowity koszt
    /// </summary>
    /// <param name="boms"></param>
    /// <returns></returns>
    public async Task<List<Bom>> GetEstimatedBomComponentsCosts(List<Bom> boms)
    {
        var defaultPeriod = _context.AccountingPeriods.SingleOrDefault(x => x.IsDefault);

        if (defaultPeriod == null)
        {
            return boms;
        }

        foreach (var item in boms)
        {

            var productCost = _context.ProductCosts
                .Include(x => x.Currency)
                .SingleOrDefault(x => x.ProductId == item.PartId && x.AccountingPeriodId == defaultPeriod.Id);

            if (productCost == null)
                continue;



            if (productCost.EstimatedCost != 0)
            {
                item.Cost = CalcutateToPln(productCost.EstimatedCost, productCost.Currency, defaultPeriod.Id);
            }
            else
            {
                item.Cost = CalcutateToPln(productCost.Cost, productCost.Currency, defaultPeriod.Id);
            }



            var qtyWithMarkup = item.PartQty * (1 + item.Excess * 0.01M);

            // jeżeli jest tylko całkowity koszt
            // to w całości wrzucamy go w materiały
            if (productCost.EstimatedLabourCost == 0 && productCost.EstimatedMaterialCost == 0)
            {
                productCost.EstimatedMaterialCost = item.Cost;
            }


            // koszty jednostkowe
            item.LabourCost = productCost.EstimatedLabourCost != 0 ? productCost.EstimatedLabourCost : productCost.LabourCost;
            item.MaterialCost = productCost.EstimatedMaterialCost != 0 ? productCost.EstimatedMaterialCost : productCost.MaterialCost;
            item.MarkupCost = productCost.EstimatedMarkupCost != 0 ? productCost.EstimatedMarkupCost : productCost.MarkupCost;




            // koszty całkowite
            item.TotalCost = item.Cost * qtyWithMarkup;
            item.TotalLabourCost = item.LabourCost * qtyWithMarkup;
            item.TotalMaterialCost = item.MaterialCost * qtyWithMarkup;
            item.TotalMarkupCost = item.MarkupCost * qtyWithMarkup;

            // informacje 
            item.CostDescription = productCost.Description;
            item.ImportedDate = productCost.ImportedDate;
            item.CalculatedDate = productCost.CalculatedDate;

        }

        return boms;
    }





    /// <summary>
    /// Przelicza wartość w walucie na PLN
    /// wg kursu z podanego okresu
    /// </summary>
    /// <param name="cost"></param>
    /// <param name="currency"></param>
    /// <param name="periodId"></param>
    /// <returns></returns>
    private decimal CalcutateToPln(decimal cost, Currency currency, int periodId)
    {
        decimal costInPln = 0;
        int pln = 1;

        if (currency != null)
        {
            if (currency.Id == pln)
            {
                costInPln = cost;
            }
            else
            {
                var rate = _context.CurrencyRates
                    .SingleOrDefault(x => x.AccountingPeriodId == periodId && x.FromCurrencyId == currency.Id);


                if (rate != null)
                {
                    var x = Math.Round(cost * rate.Rate, 2);
                    costInPln = x;
                }

            }
        }
        return costInPln;
    }

}
