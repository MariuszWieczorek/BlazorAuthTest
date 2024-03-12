using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Extensions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Enums;
using System.Globalization;
using System.IO;


namespace MwTech.Application.Services.ProductCsvService;

public partial class ProductCsvService : IProductCsvService
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<ProductCsvService> _logger;
    private readonly IProductService _productService;

    //private readonly String _path = @".\ExcelFiles\";
    private readonly string _path = @"\\Kab-svr-fs02\office\Aplikacje\mwbase\pliki_csv\";
    private readonly string _pathOkc = @"maszyna_plaska_csv\";
    private readonly string _pathOka = @"kapy_csv\";
    private readonly string _pathOdr = @"maszyna_do_drutowki_csv\";
    private readonly string _pathOwu = @"maszyny_do_wulkanizacji_opon_csv_1\";
    private readonly string _pathOrders = @"zlecenia_csv\";


    public ProductCsvService(IApplicationDbContext context,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUser,
        ILogger<ProductCsvService> logger,
        IProductService productService
        )
    {
        _context = context;
        _dateTimeService = dateTimeService;
        _currentUser = currentUser;
        _logger = logger;
        _productService = productService;
    }


    public async Task GenerateCsv(int productId, int productSettingVersionId, CsvTrigger csvTrigger)
    {

        var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

        var product = _context.Products
            .Include(x => x.ProductCategory)
            .AsNoTracking()
            .SingleOrDefault(x => x.Id == productId);


        


        if (product == null)
        {
            return;
        }

        if (product.ProductCategory.CategoryNumber == "ODR")
        {


            string pathNowaRecepta = _path + _pathOdr + "NowaRecepta";
            if (!File.Exists(pathNowaRecepta))
            {
                WriteToFile("x", _pathOdr + "NowaRecepta");
            }
            await GenerateOdrCsv(productId, productSettingVersionId, csvTrigger);

        }

        if (product.ProductCategory.CategoryNumber == "OKA")
        {


            string pathNowaRecepta = _path + _pathOka + "NowaRecepta";
            if (!File.Exists(pathNowaRecepta))
            {
                WriteToFile("x", _pathOka + "NowaRecepta");
            }
            await GenerateOkaCsv(productId, productSettingVersionId, csvTrigger);

        }

        if (product.ProductCategory.CategoryNumber == "OKC")
        {
            string pathNowaRecepta = _path + _pathOkc + "NowaRecepta";
            if (!File.Exists(pathNowaRecepta))
            {
                WriteToFile("x", _pathOkc + "NowaRecepta");
            }
            await GenerateOkcCsv(productId, productSettingVersionId, csvTrigger);

        }

        if (product.ProductCategory.CategoryNumber == "OWU")
        {
            string pathNowaRecepta = _path + _pathOkc + "NowaRecepta";
            if (!File.Exists(pathNowaRecepta))
            {
                WriteToFile("x", _pathOwu + "NowaRecepta");
            }
            await GenerateOwuCsv(productId, productSettingVersionId, csvTrigger);

        }

    }
    private async void WriteToFile(string line, string fileName)
    {
        string fileNameWithFullPath = _path + fileName;

        using (StreamWriter sw = new StreamWriter(fileNameWithFullPath, false))
        {
            await sw.WriteLineAsync(line);
        }
    }
    private async Task<Product> AddInfo(Product product)
    {
        

            if (product.ProductCategory.CategoryNumber == "OKC")
            {


                string info = string.Empty;

                var x = _context.Boms
                    .Include(x => x.Set)
                    .Where(x => x.PartId == product.Id && x.Set.TechCardNumber != null)
                    .GroupBy(x => new { x.Set.ProductNumber, x.Set.TechCardNumber, x.Layer })
                    .ToList();

                //.OrderBy(x => x.Key.TechCardNumber)
                //.ToListAsync();

                foreach (var part in x)
                {
                    info = info.Trim() + $"{part.Key.TechCardNumber}({part.Key.Layer});";
                }

                product.info = info;
            }

            if (product.ProductCategory.CategoryNumber == "ODR")
            {


                string info = string.Empty;

                var x = _context.Boms
                    .Include(x => x.Set)
                    .Where(x => x.PartId == product.Id && x.Set.TechCardNumber != null)
                    .GroupBy(x => new { x.Set.TechCardNumber })
                    .ToList();

                //.OrderBy(x => x.Key.TechCardNumber)
                //.ToListAsync();

                foreach (var part in x)
                {
                    info = info.Trim() + $"{part.Key.TechCardNumber};";
                }

                product.info = info;
            }
        

        return product;
    }


}
