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

    
private async Task GenerateOdrCsv(int productId,  int productSettingVersionId, CsvTrigger csvTrigger)
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





        var odr_IloscDrutow = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_IloscDrutow")?.Value ?? 0;
        var odr_IloscZwojow = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_IloscZwojow")?.Value ?? 0;
        var odr_Zakladka = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_Zakladka")?.Value ?? 0;
        var odr_PredkoscNawijaniaDrutu = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_PredkoscNawijaniaDrutu")?.Value ?? 0;
        var odr_CzasRozpedzaniaDrutowki = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_CzasRozpedzaniaDrutowki")?.Value ?? 0;
        var odr_CzasPrzytrzymaniaRzutuDrutowki = productSettings.
            SingleOrDefault(x => x.Setting.SettingNumber == "odr_CzasPrzytrzymaniaRzutuDrutowki")?.Value ?? 0;
        var odr_CzasPuszczaniaDrutowki = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_CzasPuszczaniaDrutowki")?.Value ?? 0;
        var odr_PozycjaRzutuDrutowki = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_PozycjaRzutuDrutowki")?.Value ?? 0;
        var odr_PozycjaZatrzymywaniaZatrzasku = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_PozycjaZatrzymywaniaZatrzasku")?.Value ?? 0;
        var odr_PojemnoscWieszaka = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_PojemnoscWieszaka")?.Value ?? 0;
        var odr_RozmiarPierscienia = productSettings
            .SingleOrDefault(x => x.Setting.SettingNumber == "odr_RozmiarPierscienia")?.Value ?? 0;




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
                IloscDrutow;{odr_IloscDrutow.ToString("G29",nfi)}
                IloscZwojow;{odr_IloscZwojow.ToString("G29", nfi)}
                Zakladka;{odr_Zakladka.ToString("G29", nfi)}
                PredkoscNawijaniaDrutu;{odr_PredkoscNawijaniaDrutu.ToString("G29",nfi)}
                CzasRozpedzaniaDrutowki;{odr_CzasRozpedzaniaDrutowki.ToString("G29", nfi)}
                CzasPrzytrzymaniaRzutuDrutowki;{odr_CzasPrzytrzymaniaRzutuDrutowki.ToString("G29", nfi)}
                CzasPuszczaniaDrutowki;{odr_CzasPuszczaniaDrutowki.ToString("G29", nfi)}
                PozycjaRzutuDrutowki;{odr_PozycjaRzutuDrutowki.ToString("G29", nfi)}
                PozycjaZatrzymywaniaZatrzasku;{odr_PozycjaZatrzymywaniaZatrzasku.ToString("G29", nfi)}
                PojemnoscWieszaka;{odr_PojemnoscWieszaka.ToString("G29", nfi)}
                RozmiarPierscienia;{odr_RozmiarPierscienia.ToString("G29", nfi)}
                WydajnoscDrutowka8Rbh;0
                WydajnoscOwijka8Rbh;0"
            .AutoTrim().TrimStart();

        //WriteToFile("", "NowaRecepta");
        WriteToFile(csvContent, _pathOdr + fileName);


        productSettingsVersion.LastCsvFileDate = _dateTimeService.Now;
        await _context.SaveChangesAsync();
    }
}

}
