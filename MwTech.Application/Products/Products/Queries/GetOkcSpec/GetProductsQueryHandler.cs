using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Products.Common;
using MwTech.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace MwTech.Application.Products.Products.Queries.GetOkcSpec;

public class GetOkcSpecQueryHandler : IRequestHandler<GetOkcSpecQuery, OkcSpecViewModel>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productWeightService;
    private readonly IProductCostService _productCostService;
    private readonly ILogger<GetOkcSpecQueryHandler> _logger;

    public GetOkcSpecQueryHandler(IApplicationDbContext context,
        IProductService productWeightService,
        IProductCostService productCostService,
        ILogger<GetOkcSpecQueryHandler> logger
        )
    {
        _context = context;
        _productWeightService = productWeightService;
        _productCostService = productCostService;
        _logger = logger;
    }
    public async Task<OkcSpecViewModel> Handle(GetOkcSpecQuery request, CancellationToken cancellationToken)
    {


        var productsCount = _context.Products
                   .Include(x => x.ProductCategory)
                   .Where(x => x.ProductCategory.CategoryNumber == "OKC")
                   .Count();


        var products = _context.Products
                    .Include(x => x.ProductCategory)
                    .Include(x => x.Unit)
                    .Where(x => x.ProductCategory.CategoryNumber == "OKC")
                    .AsNoTracking()
                    .AsQueryable();


        products = Filter(products, request.OkcFilter);


        products = products.OrderBy(x => x.ProductNumber);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = products.Count();
            request.PagingInfo.ItemsPerPage = 500;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                products = products
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var productsList = await products.ToListAsync();



        var okcSpecList = new List<OkcSpecDto>();

        foreach (var product in productsList)
        {
            var okcSpecDto = new OkcSpecDto
            {
                Id = product.Id,
                Name = product.Name,
                ProductNumber = product.ProductNumber,
                TechCardNumber = (int)product.TechCardNumber.GetValueOrDefault()
            };
            okcSpecList.Add(okcSpecDto);
        }


        foreach (var product in okcSpecList)
        {
            var productPropertiesVersion = _context.ProductPropertyVersions
                .SingleOrDefault(x => x.ProductId == product.Id && x.DefaultVersion && x.IsActive);

            if (productPropertiesVersion != null)
            {
                var productSettings = await _context.ProductProperties
                    .Include(x => x.Property)
                    .Where(x => x.ProductPropertiesVersionId == productPropertiesVersion.Id)
                    .ToListAsync();

                var okc_Szerokosc = productSettings.SingleOrDefault(x => x.Property.PropertyNumber == "okc_Szerokosc").Value ?? 0;
                var okc_KatCiecia = productSettings.SingleOrDefault(x => x.Property.PropertyNumber == "okc_KatCiecia").Value ?? 0;

                product.OkcSzerokosc = okc_Szerokosc;
                product.OkcKatCiecia = okc_KatCiecia;
            }


        }


        foreach (var product in okcSpecList)
        {

            var productVersion = _context.ProductVersions
                .Include(x => x.Accepted01ByUser)
                .Include(x => x.Accepted02ByUser)
                .SingleOrDefault(x => x.ProductId == product.Id && x.DefaultVersion);


            if (productVersion != null)
            {



                var okg = _context.Boms
                  .Include(x => x.Part)
                  .Include(x => x.Part.ProductCategory)
                  .FirstOrDefault(x => x.SetId == product.Id && x.SetVersionId == productVersion.Id && x.Part.ProductCategory.CategoryNumber == "OKG");

                if (okg != null)
                {
                    var okgPropertiesVersion = _context.ProductPropertyVersions
                           .Include(x => x.Accepted01ByUser)
                           .Include(x => x.Accepted02ByUser)
                           .SingleOrDefault(x => x.ProductId == okg.Part.Id && x.DefaultVersion);

                    if (okgPropertiesVersion != null)
                    {
                        var okgProperties = await _context.ProductProperties
                            .Include(x => x.Property)
                            .Where(x => x.ProductPropertiesVersionId == okgPropertiesVersion.Id)
                            .ToListAsync();

                        var okg_WagaGramNaM2 = okgProperties.SingleOrDefault(x => x.Property.PropertyNumber == "okg_WagaGramNaM2")?.Value ?? 0;

                        var okg_SzerokoscBalotu = okgProperties.SingleOrDefault(x => x.Property.PropertyNumber == "okg_SzerokoscBalotu")?.Value ?? 0;

                        product.OkgWagaGramNaM2 = okg_WagaGramNaM2;
                        
                        
                        product.OkcPrzelMbNaKg = okg_WagaGramNaM2  * product.OkcSzerokosc * 0.001M  * 0.001M;


                        /*
                         odległość o jaką należy przesunąć pasek kordu aby uzyskać
                         zadaną szerokość_cięcia wyliczamy ją dzieląc
                         szerokość_cięcia / sinus (kąt_cięcia)
                         */


                        var sinusAngleOfTheCut = Math.Sin( (double)product.OkcKatCiecia * Math.PI / 180);

                        double distanceBeetwenCuts = 0;
                        if (sinusAngleOfTheCut != 0)
                            distanceBeetwenCuts = Math.Round((double)product.OkcSzerokosc / sinusAngleOfTheCut, 2);
                        else
                            distanceBeetwenCuts = (double)product.OkcSzerokosc;

                        product.DistanceBeetwenCuts = (decimal)distanceBeetwenCuts;


                        double cordArea = 0;
                        if (sinusAngleOfTheCut != 0)
                            cordArea = Math.Round( ((double)product.OkcSzerokosc / sinusAngleOfTheCut) * (double)okg_SzerokoscBalotu  * 0.000001 , 5) ;
                        else
                            cordArea = Math.Round( (double)product.OkcSzerokosc * (double)okg_SzerokoscBalotu * 0.000001,5) ;

                        product.CordArea = (decimal)cordArea;
                        product.CordWeight = Math.Round((decimal)cordArea * okg_WagaGramNaM2 * 0.001M,5);

                    }


                }
            }

        }

        /*
         * k - kord gumowany  c - cięty

         masa 1 szt
         odleglosc_miedzy cieceiam
         	-- funkcja SIN() wymaga podania kąta w radianach
     
     sinus = sin(c.katCiecia*pi()/180)
     odlegloscMiedzyCieciami
	 c.szerokosc /  sinus
     else
     c.szerokosc

     dlugoscDoNawiniecia
	 k.szerokosc /  sinus
     else
     k.szerokosc

     poleSztM2	
     c.szerokosc /  sinus  * k.szerokosc * 0.001 * 0.001

     wagaSztGram 
	 c.szerokosc /  sinus  * k.szerokosc * 0.001 * 0.001 * k.wagaCel ,2) as numeric(10,2)) as 

	,CAST(  ROUND( c.szerokosc * 0.001 * k.wagaCel * 0.001,6) as numeric(10,6)) as przel_mb_na_kg 
         */


        okcSpecList = OkcFilterX(okcSpecList, request.OkcFilter);

        var vm = new OkcSpecViewModel
        {
            OkcFilter = request.OkcFilter,
            ProductCategories = await _context.ProductCategories.OrderBy(x => x.OrdinalNumber).AsNoTracking().ToListAsync(),
            Products = okcSpecList,
            PagingInfo = request.PagingInfo,
            ProductsCount = productsCount
        };


        return vm;
    }


    private IQueryable<Product> Filter(IQueryable<Product> products, OkcFilter productFilter)
    {
        if (productFilter != null)
        {
            if (!string.IsNullOrWhiteSpace(productFilter.Name))
                products = products.Where(x => x.Name.Contains(productFilter.Name.Trim()));

            if (!string.IsNullOrWhiteSpace(productFilter.ProductNumber))
                products = products.Where(x => x.ProductNumber.Contains(productFilter.ProductNumber.Trim()));

            if (!string.IsNullOrWhiteSpace(productFilter.Idx01))
                products = products.Where(x => x.Idx01.Contains(productFilter.Idx01.Trim()));

            if (!string.IsNullOrWhiteSpace(productFilter.Idx02))
                products = products.Where(x => x.Idx02.Contains(productFilter.Idx02.Trim()));

            if (productFilter.Id != 0)
                products = products.Where(x => x.Id == productFilter.Id);

            if (productFilter.TechCardNumber != 0 && productFilter.TechCardNumber != null)
                products = products.Where(x => x.TechCardNumber == productFilter.TechCardNumber);

            if (productFilter.IsActive == true)
                products = products.Where(x => x.IsActive == true);

        }
        return products;
    }


    private List<OkcSpecDto> OkcFilterX(IEnumerable<OkcSpecDto> okcList, OkcFilter okcFilter)
    {
        if (okcFilter != null)
        {

            if (okcFilter.NoOkcKatCiecia == true)
                okcList = okcList.Where(x => x.OkcKatCiecia == 0);

            if (okcFilter.NoOkcSzerokosc == true)
                okcList = okcList.Where(x => x.OkcSzerokosc == 0);

        }
        return okcList.ToList();
    }
}
