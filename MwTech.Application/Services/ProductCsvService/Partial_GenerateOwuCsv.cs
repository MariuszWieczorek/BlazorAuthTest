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

    private async Task GenerateOwuCsv(int productId, int productSettingVersionId, CsvTrigger csvTrigger)
    {

        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";

        var product = _context.Products
            .Include(x=>x.ProductCategory)
            .SingleOrDefault(x => x.Id == productId);

        if (product == null)
        {
            return;
        }


        if (product.TechCardNumber == null)
        {
            return;
        }

        product = await AddInfo(product);

        var productSettingsVersionsX =  _context.ProductSettingVersions
        .Include(x => x.Accepted01ByUser)
        .Include(x => x.Accepted02ByUser)
        .Include(x => x.Accepted03ByUser)
        .Include(x => x.WorkCenter)
        .Where(x => x.ProductId == productId && x.IsActive);
        

        if (productSettingVersionId != 0)
        {
            productSettingsVersionsX = productSettingsVersionsX
                .Where(x => x.Id == productSettingVersionId);
                
        }

        var productSettingsVersions = await productSettingsVersionsX.ToListAsync();


        foreach (var productSettingsVersion in productSettingsVersions)
        {



            var productPropertiesVersion = _context.ProductPropertyVersions
                .Include(x => x.Accepted01ByUser)
                .Include(x => x.Accepted02ByUser)
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductId == productId && x.DefaultVersion);

            var productVersion = _context.ProductVersions
                .Include(x => x.Accepted01ByUser)
                .Include(x => x.Accepted02ByUser)
                .AsNoTracking()
                .SingleOrDefault(x => x.ProductId == productId && x.DefaultVersion);


            if (productSettingsVersion == null || productVersion == null)
            {
                return;
            }

            var productSettings = await _context.ProductSettingVersionPositions
                .Include(x => x.Setting)
                .Include(x => x.Setting.Unit)
                .Where(x => x.ProductSettingVersionId == productSettingsVersion.Id)
                .OrderBy(x => x.Setting.SettingCategory.OrdinalNumber)
                .ThenBy(x => x.Setting.OrdinalNumber)
                .AsNoTracking()
                .ToListAsync();








            bool testVersion = !(productSettingsVersion.IsAccepted01 && productSettingsVersion.IsAccepted02 && productSettingsVersion.IsAccepted03)
                || !productSettingsVersion.DefaultVersion;

            string fileName = $"{productSettingsVersion.WorkCenter.ResourceNumber}-{(testVersion ? 'T' : 'Z')}-{product.TechCardNumber ?? 0}.csv";

            // version;{product.TechCardNumber}.{productSettingsVersion.AlternativeNo}.{productSettingsVersion.ProductSettingVersionNumber}.{productSettingsVersion.Rev}


            string csvHeader = $@"
                versionName;{productSettingsVersion.Name}
                versionId;{productSettingsVersion.Id}
                csvFileVersion;{productSettingsVersion.WorkCenter.ResourceNumber}.1
                symbol1Maszyny;{productSettingsVersion.WorkCenter.ResourceNumber}
                workCenterNumber;{productSettingsVersion.WorkCenter.ResourceNumber}
                workCenterName;{productSettingsVersion.WorkCenter.Name}
                productId;{product.Id}
                techCardNumber;{product.TechCardNumber ?? 0}
                productNumber;{product.ProductNumber}
                productName;{product.Name?.StripText()}
                czyTestowa;{(testVersion ? "TAK" : "NIE")}
                czyZatw1;{(productSettingsVersion.IsAccepted01 ? "TAK" : "NIE")}
                UserNameZatw1;{productSettingsVersion?.Accepted01ByUser?.FirstName} {productSettingsVersion?.Accepted01ByUser?.LastName}
                czasZatw1;DT#{productSettingsVersion.Accepted01Date.GetValueOrDefault().ToString("yyyy-MM-dd-HH:mm:ss", CultureInfo.CreateSpecificCulture("pl-PL"))}
                czyZatw2;{(productSettingsVersion.IsAccepted02 ? "TAK" : "NIE")}
                UserNameZatw2;{productSettingsVersion?.Accepted02ByUser?.FirstName} {productSettingsVersion?.Accepted02ByUser?.LastName}
                czasZatw2;DT#{productSettingsVersion.Accepted02Date.GetValueOrDefault().ToString("yyyy-MM-dd-HH:mm:ss", CultureInfo.CreateSpecificCulture("pl-PL"))}
                czyZatw3;{(productSettingsVersion.IsAccepted03 ? "TAK" : "NIE")}
                UserNameZatw3;{productSettingsVersion?.Accepted03ByUser?.FirstName} {productSettingsVersion?.Accepted03ByUser?.LastName}
                czasZatw3;DT#{productSettingsVersion.Accepted03Date.GetValueOrDefault().ToString("yyyy-MM-dd-HH:mm:ss", CultureInfo.CreateSpecificCulture("pl-PL"))}
                czasZapisuCSV;DT#{_dateTimeService.Now.ToString("yyyy-MM-dd-HH:mm:ss", CultureInfo.CreateSpecificCulture("pl-PL"))}
                uwagi;{productSettingsVersion.Description?.StripText() ?? "Brak dodatkowych informacji."}"
                    .AutoTrim().TrimStart();


            string csvPositions = string.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (var item in productSettings)
            {
                var val = item?.Value ?? 0;
                if (item.Value == null && !String.IsNullOrEmpty(item.Text)) 
                    sb.Append($"{item.Setting.SettingNumber};{item.Text.StripText()}\n");
                else
                    sb.Append($"{item.Setting.SettingNumber};{val.ToString("G29", nfi)}\n");
            }

            csvPositions = sb.ToString();

            string csvContent = $"{csvHeader}\n{csvPositions}";

            WriteToFile(csvContent, _pathOwu + fileName);


            productSettingsVersion.LastCsvFileDate = _dateTimeService.Now;
            await _context.SaveChangesAsync();
        }
    }

}
