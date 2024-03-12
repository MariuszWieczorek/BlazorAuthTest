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


/* Generowanie pliku csv dla Kordów Ciętych */

public partial class ProductCsvService
{
    
private async Task GenerateOkcCsv(int productId,  int productSettingVersionId, CsvTrigger csvTrigger)
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
     
        var productSettings = await _context.ProductSettingVersionPositions
            .Include(x => x.Setting)
            .Include(x => x.Setting.Unit)
            .Where(x => x.ProductSettingVersionId == productSettingsVersion.Id)
            .ToListAsync();


        var okg = _context.Boms
       .Include(x => x.Part)
       .Include(x => x.Part.ProductCategory)
       .FirstOrDefault(x => x.SetId == product.Id && x.SetVersionId == productVersion.Id && x.Part.ProductCategory.CategoryNumber == "OKG");

        if (okg == null)
        {
            return;
        }

        var okgPropertiesVersion = _context.ProductPropertyVersions
           .Include(x => x.Accepted01ByUser)
           .Include(x => x.Accepted02ByUser)
           .SingleOrDefault(x => x.ProductId == okg.Part.Id && x.DefaultVersion);


        if (okgPropertiesVersion == null)
        {
            return;
        }


        var okgProperties = await _context.ProductProperties
            .Include(x => x.Property)
            .Where(x => x.ProductPropertiesVersionId == okgPropertiesVersion.Id)
            .ToListAsync();



        var okc_Szerokosc = productSettings.SingleOrDefault(x => x.Setting.SettingNumber == "okc_Szerokosc").Value ?? 0;
        var okc_KatCiecia = productSettings.SingleOrDefault(x => x.Setting.SettingNumber == "okc_KatCiecia").Value ?? 0;
        double cutDistance;

        //ROUND( case when sin(c.katCiecia*pi()/180) != 0 then  c.szerokosc /  sin(c.katCiecia*pi()/180) else c.szerokosc end ,2) as numeric(10,2)) as odlegloscMiedzyCieciami

        var sineOfTheAngle = Math.Sin((double)okc_KatCiecia * Math.PI / 180);
        if (sineOfTheAngle != 0)
        {
            cutDistance = Math.Round((double)okc_Szerokosc / sineOfTheAngle, 0);
        }
        else
        {
            cutDistance = (double)okc_Szerokosc;
        }

        var okg_SzerokoscBalotu = okgProperties.SingleOrDefault(x => x.Property.PropertyNumber == "okg_SzerokoscBalotu")?.Value ?? 0;
        var okg_GruboscGumowania = okgProperties.SingleOrDefault(x => x.Property.PropertyNumber == "okg_GruboscGumowania")?.Value ?? 0;
        var okg_WagaGramNaM2 = okgProperties.SingleOrDefault(x => x.Property.PropertyNumber == "okg_WagaGramNaM2")?.Value ?? 0;
            
        var okc_PrzelMbNaKg = okg_WagaGramNaM2  * okc_Szerokosc * 0.001M  * 0.001M;


        bool testVersion = !(productSettingsVersion.IsAccepted01 && productSettingsVersion.IsAccepted02 && productSettingsVersion.IsAccepted03);
        string fileName = $"{productSettingsVersion.WorkCenter.ResourceNumber}-{(testVersion ? 'T' : 'Z')}-{product.TechCardNumber ?? 0}.csv";

        // version;{product.TechCardNumber}.{productSettingsVersion.AlternativeNo}.{productSettingsVersion.ProductSettingVersionNumber}.{productSettingsVersion.Rev}

        string csvContent = $@"
                versionName;{productSettingsVersion.Name}.{productSettingsVersion.Rev}
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
                NrWarstwy;1
                WagaGramNaM2;{okg_WagaGramNaM2.ToString("G29",nfi)}
                KordGumowanyIndeks;{okg?.Part?.ProductNumber}
                KordGumowanyNazwa;{okg?.Part?.Name?.StripText()}
                SzerokoscBalotu;{(int)okg_SzerokoscBalotu}
                KordCietyIndeks;{product.ProductNumber}
                KordCietyNazwa;{product.Name}
                KatCiecia;{okc_KatCiecia.ToString("G29",nfi)}
                Szerokosc;{(int)okc_Szerokosc}
                GruboscGumowania;{okg_GruboscGumowania.ToString("G29",nfi)}
                OdlegloscMiedzyCieciami;{cutDistance}
                PrzelicznikMbNaKg;{okc_PrzelMbNaKg.ToString("G29",nfi)}"
            .AutoTrim().TrimStart();

        //WriteToFile("", "NowaRecepta");
        WriteToFile(csvContent, _pathOkc + fileName);


        productSettingsVersion.LastCsvFileDate = _dateTimeService.Now;
        await _context.SaveChangesAsync();
    }
}

}
