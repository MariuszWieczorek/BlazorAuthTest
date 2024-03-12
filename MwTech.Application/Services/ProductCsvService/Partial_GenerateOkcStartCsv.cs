using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Extensions;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using MwTech.Domain.Enums;
using System.Globalization;
using System.IO;
using System.Text;

namespace MwTech.Application.Services.ProductCsvService;


/* Generowanie pliku csv dla Kordów Ciętych */

public partial class ProductCsvService
{

    public async Task GenerateStartOrderCsv(string productNumber, decimal qty, int orderNumber, string workCenterNumber, int operationId = 0)
    {

        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";


        var product = _context.Products.SingleOrDefault(x => x.ProductNumber == productNumber);

        if (product == null)
        {
            return;
        }


        if (product.TechCardNumber == null)
        {
            return;
        }


        if (workCenterNumber == null)
        {
            return;
        }



        string fileName = $"{workCenterNumber}-0.csv";

        // version;{product.TechCardNumber}.{productSettingsVersion.AlternativeNo}.{productSettingsVersion.ProductSettingVersionNumber}.{productSettingsVersion.Rev}

        string csvHeader = $@"
                TechCardNumber;{product.TechCardNumber ?? 0}
                Qty;{qty}
                WorkCenterNumber;{workCenterNumber}
                OrderNumber;{orderNumber}
                OperationId;{operationId}
                ProductNumber;{product.ProductNumber}
                ProductName;{product.Name?.StripText()}
                CzasZapisuCSV;DT#{_dateTimeService.Now.ToString("yyyy-MM-dd-HH:mm:ss", CultureInfo.CreateSpecificCulture("pl-PL"))}"
                .AutoTrim().TrimStart();

        var componentUsages = await GetComponentUsages(productNumber);


        StringBuilder sb = new StringBuilder();
        int i = 1;

        foreach (var item in componentUsages)
        {
           sb.Append($"info{i};{item.ProductName?.StripText()}\n");
            i++;
        }

        var csvPositions = sb.ToString();

        string csvContent = $"{csvHeader}\n{csvPositions}";



        WriteToFile(csvContent, _pathOrders + fileName);

        string pathNowaRecepta = _path + _pathOrders + "NowaRecepta";
        if (!File.Exists(pathNowaRecepta))
        {
            WriteToFile("x", _pathOrders + "NowaRecepta");
        }

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


