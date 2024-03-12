using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.Services.ProductCostService;

public class ProductCostService : IProductCostService
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<ProductCostService> _logger;
    private readonly IProductService _productService;
    private readonly AccountingPeriod _defaultPeriod;

    private class UnitProductCost
    {
        public decimal LabourCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal ProductFee { get; set; }

        public decimal TotalCost()
        {
            return MaterialCost + LabourCost + ProductFee; 
        }
    }

    public ProductCostService(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<ProductCostService> logger,
        IProductService productService
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _productService = productService;
        _defaultPeriod = _context.AccountingPeriods
            .AsNoTracking()
            .SingleOrDefault(x => x.IsDefault);

        if (_defaultPeriod == null)
            throw new Exception("Brak domyślnego okresu !");
    }

    public async Task CaluclateProductCost(int productId)
    {


        var product = _context.Products
            .Include(x => x.ProductCategory)
            .AsNoTracking()
            .SingleOrDefault(x => x.Id == productId);


        if (product.NoCalculateTkw || product.ProductCategory.NoCalculateTkw || !product.IsActive)
            return;


        var unitProductCost = new UnitProductCost
        {
            MaterialCost = await CalculateUnitMaterialCost(productId),
            LabourCost = await CalculateUnitLabourCost(product),
            // ProductFee = await GetProductFee(product)
        };
        
        await SaveProductCost(productId, unitProductCost);

    }

    public async Task CaluclateOnlyMaterialProductCost(int productId)
    {


        var product = _context.Products
            .Include(x => x.ProductCategory)
            .AsNoTracking()
            .SingleOrDefault(x => x.Id == productId);


        if (product.NoCalculateTkw || product.ProductCategory.NoCalculateTkw || !product.IsActive)
            return;


        var unitProductCost = new UnitProductCost
        {
            MaterialCost = await CalculateUnitMaterialCost(productId),
            LabourCost = 0M,
            ProductFee = 0M
        };

        await SaveProductCost(productId, unitProductCost);

    }

    private async Task SaveProductCost(int productId, UnitProductCost unitProductCost)
    {
        var productCost = _context.ProductCosts
                    .AsNoTracking()
                    .SingleOrDefault(x => x.ProductId == productId && x.AccountingPeriodId == _defaultPeriod.Id);


        bool newProductCost = false;



        if (productCost == null)
        {
            productCost = new ProductCost
            {
                ProductId = productId,
                AccountingPeriodId = _defaultPeriod.Id,
                CreatedByUserId = _currentUser.UserId
            };
            newProductCost = true;
        }

        productCost.Cost = unitProductCost.TotalCost();
        productCost.MaterialCost = unitProductCost.MaterialCost;
        productCost.LabourCost = unitProductCost.LabourCost;
        productCost.CurrencyId = 1;
        productCost.IsCalculated = true;
        productCost.CalculatedDate = _dateTimeService.Now;


        if (newProductCost)
        {
            productCost.CreatedByUserId = _currentUser.UserId;
            productCost.CreatedDate = _dateTimeService.Now;
            await _context.ProductCosts.AddAsync(productCost);
        }
        else
        {
            productCost.ModifiedByUserId = _currentUser.UserId;
            productCost.ModifiedDate = _dateTimeService.Now;
            _context.ProductCosts.Update(productCost);
        }

        await _context.SaveChangesAsync();
    }

    #region Kalkulacja Robocizny


    private async Task<decimal> CalculateUnitLabourCost(Product product)
    {
        decimal calculatedUnitLabourCost = 0M;

        RouteVersion defaultRouteVersion = null;

        if (product.ProductCategory.RouteSource == 0 )
        {
            defaultRouteVersion = _context.RouteVersions
                    .AsNoTracking()
                    .FirstOrDefault(x => 
                    x.ProductId == product.Id 
                    && x.DefaultVersion
                    && x.IsActive
                    );
        }


        /* Modyfikacja pobieranie marszruty z węża, jeżeli nie ma przy indeksie głównym */
        if (product.ProductCategory.RouteSource == 2 )
        {
            if (product.Idx02 != null)
            {
                var parentProduct = _context.Products
                .Include(x => x.ProductCategory)
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductNumber == product.Idx02);

                if (parentProduct != null)
                    defaultRouteVersion = _context.RouteVersions
                            .Include(x => x.Product)
                            .AsNoTracking()
                            .FirstOrDefault(x => x.ProductId == parentProduct.Id
                            && x.ProductCategoryId == product.ProductCategoryId
                            && x.DefaultVersion
                            && x.IsActive
                            );
            }
        }

        /* Modyfikacja pobieranie marszruty z matki, jeżeli nie ma przy indeksie głównym */
        if (product.ProductCategory.RouteSource == 1)
        {
            if (product.Idx01 != null)
            {
                var parentProduct = _context.Products
                .Include(x => x.ProductCategory)
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductNumber == product.Idx01);

                if (parentProduct != null)
                    defaultRouteVersion = _context.RouteVersions
                            .Include(x => x.Product)
                            .AsNoTracking()
                            .FirstOrDefault(x => x.ProductId == parentProduct.Id
                            && x.ProductCategoryId == product.ProductCategoryId
                            && x.DefaultVersion
                            && x.IsActive
                            );
            }
        }


        if (defaultRouteVersion == null)
        {
            _logger.LogWarning($"Kalkulacja kosztów: brak domyślnej wersji marszruty {product.Id}");
        }
        else
        {
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


            decimal totalRouteCost = await GetTotalRouteCost(product.Id,routes);
            
            // decimal routeTotalQty = defaultRouteVersion.ProductQty == 0 ? 1 : defaultRouteVersion.ProductQty;
            // calculatedUnitLabourCost = totalRouteCost / routeTotalQty;
            calculatedUnitLabourCost = totalRouteCost;
        }

        return calculatedUnitLabourCost;
    }

    private async Task<decimal> GetTotalRouteCost(int productId, List<ManufactoringRoute> routes)
    {

        routes = await GetRoutesCost(routes);
        decimal totalRouteCost = routes.Sum(x => x.TotalCost);

        // jeżeli podany narzut na kg to liczymy inaczej

        var calculatePerKg = routes.Any(x =>
                x.Resource.ResourceNumber == "PC.BO.POP" ||
                x.Resource.ResourceNumber == "PC.BO.WF");


        if (calculatePerKg)
        {
            decimal productWeight = await _productService.CalculateWeight(productId, 0);
            decimal fee = 0;

            var firstRoute = routes
                .FirstOrDefault(x =>
                x.Resource.ResourceNumber == "PC.BO.POP" ||
                x.Resource.ResourceNumber == "PC.BO.WF");

            // 2023.12.21 bo narzut mamy wziąść tylko raz

            if (firstRoute != null)
            {
                fee = firstRoute.Resource.Markup  * productWeight;
            }


            totalRouteCost = totalRouteCost + fee;
        }


        return totalRouteCost;
    }

    public async Task<List<ManufactoringRoute>> GetRoutesCost(List<ManufactoringRoute> routes)
    {
        foreach (var item in routes)
        {

            // koszt kategoria zaszeregowania, koszt gniazda
            var resourceCost = item?.Resource?.Cost ?? 0m;
            var workCenterCost = item?.WorkCenter?.Cost ?? 0m;


            decimal labourMarkup = item.Resource.Markup * 0.01m;
            decimal labourCostWithoutMarkup = 0m;
            decimal totalLabourCost = 0M;

            decimal workCenterMarkup = item.WorkCenter.Markup * 0.01m;
            decimal workCenterCostWithoutMarkup = 0m;
            decimal totalWorkCenterCost;

            var resourceUnitCode = item?.Resource?.Unit?.UnitCode;

            // czas w części godziny na jm lub wydajność jm na godzinę
            var operationUnitCode = item?.Operation?.Unit?.UnitCode;


            bool calculatePerKg = (
                item.Resource.ResourceNumber == "PC.BO.POP" ||
                item.Resource.ResourceNumber == "PC.BO.WF");



            if (operationUnitCode == "Godz./jedn.")
            {
                labourCostWithoutMarkup = item.ResourceQty * resourceCost * item.OperationLabourConsumption;
                workCenterCostWithoutMarkup = workCenterCost * item.OperationMachineConsumption;
            }


            if (operationUnitCode == "Jedn./godz.")
            {
                labourCostWithoutMarkup = item.OperationLabourConsumption != 0 ? item.ResourceQty * resourceCost / item.OperationLabourConsumption : 0m;
                workCenterCostWithoutMarkup = item.OperationMachineConsumption != 0 ? workCenterCost / item.OperationMachineConsumption : 0m;
            }


            // wyjątek dla mieszanek
            if (resourceUnitCode == "pln/sec")
                labourCostWithoutMarkup = item.ResourceQty * resourceCost * item.OperationLabourConsumption;


            //todo sprawdzić zaokrąglenia
            // zaokrąglam tylko raz na końcu
            labourCostWithoutMarkup = Math.Round(labourCostWithoutMarkup, 5);
            workCenterCostWithoutMarkup = Math.Round(workCenterCostWithoutMarkup, 5);


            totalWorkCenterCost = Math.Round(workCenterCostWithoutMarkup * (1 + workCenterMarkup), 5);


            // inaczej liczymy narzut dla Bolechowa
            // narzut dodamy później
            if (!calculatePerKg)
                totalLabourCost = Math.Round(labourCostWithoutMarkup * (1 + labourMarkup), 5);
            else
                totalLabourCost = labourCostWithoutMarkup;



            item.CostWithoutMarkup = labourCostWithoutMarkup + workCenterCostWithoutMarkup;
            item.TotalCost = totalLabourCost + totalWorkCenterCost;
        }


        return routes;
    }

    #endregion

    #region Kalkulacja Materiałów
    private async Task<decimal> CalculateUnitMaterialCost(int productId)
    {
        decimal calculatedUnitMaterialCost = 0M;

        var defaultProductVersion = _context.ProductVersions
            .AsNoTracking()
            .FirstOrDefault(x => x.ProductId == productId && x.DefaultVersion);

        if (defaultProductVersion == null)
        {
            _logger.LogWarning($"Kalkulacja kosztów: brak BOM {productId}");
        }
        else
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
                             .Where(x => x.SetId == productId
                              && x.SetVersionId == defaultProductVersion.Id
                              && x.DoNotExportToIfs == false
                              && x.OnProductionOrder)
                             .OrderBy(x => x.OrdinalNumber)
                             .ThenBy(x => x.Part.ProductNumber)
                             .AsNoTracking()
                             .ToListAsync();


            boms = await GetBomComponentsCosts(boms);

            decimal totalMaterialCost = boms.Sum(x => x.TotalCost);
            decimal productTotalQty = defaultProductVersion.ProductQty == 0 ? 1 : defaultProductVersion.ProductQty;
            calculatedUnitMaterialCost = totalMaterialCost / productTotalQty;
        }

        return calculatedUnitMaterialCost;
    }

     public async Task<List<Bom>> GetBomComponentsCosts(List<Bom> boms)
    {
        var defaultPeriod = _defaultPeriod;

        if (defaultPeriod == null)
        {
            return boms;
        }

        foreach (var item in boms)
        {

            var productCost = _context.ProductCosts
                .Include(x => x.Currency)
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductId == item.PartId && x.AccountingPeriodId == defaultPeriod.Id);

            if (productCost != null)
            {
                var costInPln = CalcutateToPln(productCost.Cost, productCost.Currency, defaultPeriod.Id);
                decimal finalExcess = 0m;

                if (item.Set.ProductCategory.TkwCountExcess)
                {
                    finalExcess = item.Excess;
                }

                item.Cost = costInPln;
                item.TotalCost = costInPln * (item.PartQty + item.PartQty * finalExcess * 0.01M);
                item.LabourCost = productCost.LabourCost;
                item.MaterialCost = productCost.MaterialCost;
                item.MarkupCost = productCost.MarkupCost;
                item.CostDescription = productCost.Description;
                item.ImportedDate = productCost.ImportedDate;
                item.CalculatedDate = productCost.CalculatedDate;
            }
        }

        return boms;
    }


     #endregion


    #region Pobieranie cen do listy wyrobów i drzewka 

    public async Task<List<Product>> GetProductsCost(List<Product> products)
    {
        var defaultPeriod = _context.AccountingPeriods.SingleOrDefault(x => x.IsDefault);

        foreach (var item in products)
        {
            var productCost = _context.ProductCosts
                .Include(x => x.Currency)
                .FirstOrDefault(x => x.ProductId == item.Id && x.AccountingPeriodId == defaultPeriod.Id);

            if (productCost != null)
            {
                item.Cost = CalcutateToPln(productCost.Cost, productCost.Currency, defaultPeriod.Id);
                item.LabourCost = productCost.LabourCost;
                item.MaterialCost = productCost.MaterialCost;
            }

        }

        return products;
    }
    
    public async Task<List<BomTree>> GetBomTreesComponentsCosts(List<BomTree> bomTrees)
    {
        var defaultPeriod = _defaultPeriod;

        if (defaultPeriod == null)
        {
            return bomTrees;
        }

        foreach (var item in bomTrees)
        {

            var productCost = _context.ProductCosts
                .Include(x => x.Currency)
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductId == item.PartProductId && x.AccountingPeriodId == defaultPeriod.Id);

            if (productCost != null)
            {
                var costInPln = CalcutateToPln(productCost.Cost, productCost.Currency, defaultPeriod.Id);
                decimal finalExcess = 0m;


                item.Cost = Math.Round(costInPln,2);
                item.TotalCost = costInPln * (item.PartProductQty + item.PartProductQty * finalExcess * 0.01M);
                
                item.LabourCost = Math.Round(productCost.LabourCost,2);
                item.MaterialCost = Math.Round(productCost.MaterialCost,2);
                item.MarkupCost = productCost.MarkupCost;
                item.CostDescription = productCost.Description;
                item.ImportedDate = productCost.ImportedDate;
                item.CalculatedDate = productCost.CalculatedDate;
                item.PartCost = Math.Round(costInPln * item.FinalPartProductQty,2);
            }
        }

        return bomTrees;
    }

    #endregion


    public decimal CalcutateToPln(decimal cost, Currency currency, int periodId)
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
                    .AsNoTracking()
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

    private async Task<decimal> GetProductFee(Product product)
    {
        // Opłata produktowa przy sprzedaży opon 0.07 zł/kg
        // [16.12 10:36] Mariusz Wieczorek
        // lnOplataProduktowaZaOpone = ROUND(lnOplataProduktowaZaKg * wulk.calkowitaWagaOpony * 0.001, 3)
        // tak mam w MwTech, *0,001 bo waga tam w gramach
        // 0,070 za kg

        decimal productFee = 0M;

        if (product == null)
        {
            return 0M;
        }

        if (product.ProductCategory.CategoryNumber != "OWU")
        {
            return 0M;
        }

        decimal productWeight = await _productService.CalculateWeight(product.Id, 0);
        productFee = Math.Round(productWeight * 0.07M, 3);

        return productFee;
    }

    public async Task<decimal> GetProductPrice(int productId)
    {

        decimal productPrice = 0M;

        if (productId == 0)
            return productPrice;


        var productCost = _context.ProductCosts
                .Include(x => x.Currency)
                .SingleOrDefault(x => x.ProductId == productId && x.AccountingPeriodId == _defaultPeriod.Id);

        if (productCost != null)
            productPrice = CalcutateToPln(productCost.Cost, productCost.Currency, _defaultPeriod.Id);




        return productPrice;
    }

}
