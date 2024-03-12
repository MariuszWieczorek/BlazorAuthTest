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


/* Generowanie pliku csv dla Drutówek */
public partial class ProductCsvService
{


    private async Task GenerateOkaCsv(int productId, int productSettingVersionId, CsvTrigger csvTrigger)
    {

        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";

        var product = _context.Products
                .Include(x => x.ProductCategory)
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

        var productSettingsVersionsX = _context.ProductSettingVersions
        .Include(x => x.Accepted01ByUser)
        .Include(x => x.Accepted02ByUser)
        .Include(x => x.Accepted03ByUser)
        .Include(x => x.WorkCenter)
        .Where(x => x.ProductId == productId && x.DefaultVersion && x.IsActive);


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
                .SingleOrDefault(x => x.ProductId == product.Id && x.DefaultVersion);

            var productVersion = _context.ProductVersions
                .Include(x => x.Accepted01ByUser)
                .Include(x => x.Accepted02ByUser)
                .SingleOrDefault(x => x.ProductId == product.Id && x.DefaultVersion);


            if (productSettingsVersion == null || productVersion == null)
            {
                return;
            }


            var mie = _context.Boms
                        .Include(x => x.Part)
                        .Include(x => x.Part.ProductCategory)
                        .FirstOrDefault(x => x.SetId == product.Id && x.SetVersionId == productVersion.Id && x.Part.ProductCategory.CategoryNumber == "MIE");

            if (mie == null)
            {
                return;
            }


            var mieszankaIndeks = mie.Part?.ProductNumber;
            var mieszankaIlosc = mie.PartQty;


            var productSettings = await _context.ProductSettingVersionPositions
                .Include(x => x.Setting)
                .Include(x => x.Setting.Unit)
                .Where(x => x.ProductSettingVersionId == productSettingsVersion.Id)
                .ToListAsync();


            var productProperties = await _context.ProductProperties
            .Include(x => x.Property)
            .Where(x => x.ProductPropertiesVersionId == productPropertiesVersion.Id)
            .ToListAsync();


            var oka_szerokosc = productProperties
                .SingleOrDefault(x => x.Property.PropertyNumber == "oka_szerokosc")?.Value ?? 0;

            var oka_szerokosc_min = productProperties
                .SingleOrDefault(x => x.Property.PropertyNumber == "oka_szerokosc")?.MinValue ?? 0;

            var oka_szerokosc_max = productProperties
                .SingleOrDefault(x => x.Property.PropertyNumber == "oka_szerokosc")?.MaxValue ?? 0;

            var oka_grubosc = productProperties
                .SingleOrDefault(x => x.Property.PropertyNumber == "oka_grubosc")?.Value ?? 0;

            var oka_grubosc_min = productProperties
                .SingleOrDefault(x => x.Property.PropertyNumber == "oka_grubosc")?.MinValue ?? 0;

            var oka_grubosc_max = productProperties
                .SingleOrDefault(x => x.Property.PropertyNumber == "oka_grubosc")?.MaxValue ?? 0;

            /*
            var oka_szerokosc = productSettings
                .SingleOrDefault(x => x.Setting.SettingNumber == "oka_szerokosc")?.Value ?? 0;
            */




            bool testVersion = !(productSettingsVersion.IsAccepted01 && productSettingsVersion.IsAccepted02 && productSettingsVersion.IsAccepted03);
            string fileName = $"{productSettingsVersion.WorkCenter.ResourceNumber}-{(testVersion ? 'T' : 'Z')}-{product.TechCardNumber ?? 0}.csv";

            string csvContent = $@"
                version;{product.TechCardNumber}.{productSettingsVersion.AlternativeNo}.{productSettingsVersion.ProductSettingVersionNumber}.{productSettingsVersion.Rev}
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
                uwagi;{productSettingsVersion.Description ?? "Brak dodatkowych informacji."}
                SzerokoscCel;{oka_szerokosc.ToString("G29", nfi)}
                SzerokoscMin;{oka_szerokosc_min.ToString("G29", nfi)}
                SzerokoscMax;{oka_szerokosc_max.ToString("G29", nfi)}
                GruboscCel;{oka_grubosc.ToString("G29", nfi)}
                GruboscMin;{oka_grubosc_min.ToString("G29", nfi)}
                GruboscMax;{oka_grubosc_max.ToString("G29", nfi)}
                MieszankaIndeks;{mieszankaIndeks}
                MieszankaIlosc;{mieszankaIlosc.ToString("G29", nfi)}"
                .AutoTrim().TrimStart();

            //WriteToFile("", "NowaRecepta");
            WriteToFile(csvContent, _pathOka + fileName);


            productSettingsVersion.LastCsvFileDate = _dateTimeService.Now;
            await _context.SaveChangesAsync();
        }
    }

}
