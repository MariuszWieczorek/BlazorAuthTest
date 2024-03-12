using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.Word;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.ProductSettingsVersionsPositions.Commands.ImportProductSettingVersionPositionFromExcel;

public class ImportSettingsFromExcelCommandHandler : IRequestHandler<ImportSettingsFromExcelCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ImportSettingsFromExcelCommandHandler> _logger;

    public ImportSettingsFromExcelCommandHandler(IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        ILogger<ImportSettingsFromExcelCommandHandler> logger

        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }
    public async Task Handle(ImportSettingsFromExcelCommand request, CancellationToken cancellationToken)
    {

        string fileName = request.FileName;

        if (!File.Exists(fileName))
        {
            throw new Exception($"Brak pliku {fileName}");
        }

        int counter = 1;

        var settingsToImport = await GetSettingsFromExcel(fileName);

        var currentUserId = _currentUserService.UserId;

        if (settingsToImport.Count() == 0)
            throw new Exception("Brak ustawień do zaimportowania");


        var versions = settingsToImport
            .GroupBy(x => new { x.ProductId, x.MachineId, x.WorkCenterId, x.MachineCategoryId, x.VersionNo, x.VersionName, x.AltNo });


        foreach (var item in versions)
        {



            ProductSettingVersion producSettingVersion = null;
            bool defaultVersion = item.Key.VersionNo == 1;
            bool newVersion = false;



            producSettingVersion = await _context.ProductSettingVersions
              .SingleOrDefaultAsync(x => x.ProductId == item.Key.ProductId && x.ProductSettingVersionNumber == item.Key.VersionNo && x.AlternativeNo == item.Key.AltNo);

            newVersion = producSettingVersion == null;


            if (newVersion)
            {
                producSettingVersion = new ProductSettingVersion
                {
                    Name = item.Key.VersionName,
                    ProductId = item.Key.ProductId,
                    MachineId = item.Key.MachineId,
                    WorkCenterId = item.Key.WorkCenterId,
                    MachineCategoryId = item.Key.MachineCategoryId,
                    ProductSettingVersionNumber = item.Key.VersionNo,
                    AlternativeNo = item.Key.AltNo,
                    CreatedByUserId = _currentUserService.UserId,
                    CreatedDate = _dateTimeService.Now,
                    DefaultVersion = defaultVersion,
                };
            }



            var settings = settingsToImport
             .Where(x => x.ProductId == item.Key.ProductId && x.VersionNo == item.Key.VersionNo && x.AltNo == item.Key.AltNo);


            foreach (var setting in settings)
            {

                counter++;

                var settingToUpdate = await _context.ProductSettingVersionPositions
                    .Include(x => x.ProductSettingVersion)
                    .SingleOrDefaultAsync(x =>
                    x.ProductSettingVersion.AlternativeNo == setting.AltNo &&
                    x.ProductSettingVersionId == setting.VersionNo &&
                    x.ProductSettingVersion.ProductId == setting.ProductId &&
                    x.SettingId == setting.SettingId
                    );


                if (settingToUpdate == null)
                {
                    var settingToAdd = new ProductSettingVersionPosition()
                    {
                        ProductSettingVersion = producSettingVersion,
                        SettingId = setting.SettingId,
                        MinValue = setting.MinValue,
                        Value = setting.Value,
                        MaxValue = setting.MaxValue,
                        Text = setting.Text
                    };

                    _context.ProductSettingVersionPositions.Add(settingToAdd);
                }
                else
                {
                    settingToUpdate.MinValue = setting.MinValue;
                    settingToUpdate.Value = setting.Value;
                    settingToUpdate.MaxValue = setting.MaxValue;
                    settingToUpdate.Text = setting.Text;
                }


                await _context.SaveChangesAsync();

            }

        }
        return;
    }


    private async Task<IEnumerable<SettingFromExcelToImport>> GetSettingsFromExcel(string fileName)
    {

        List<SettingFromExcelToImport> settingsToImport = new List<SettingFromExcelToImport>();

        var settings = await _context.Settings.AsNoTracking().ToListAsync();
        var products = await _context.Products.AsNoTracking().ToListAsync();

        int counter = 1;

        using (var excelWorkbook = new XLWorkbook(fileName))
        {
            var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();



            foreach (var dataRow in nonEmptyDataRows)
            {
                counter++;

                if (dataRow.RowNumber() >= 2 && dataRow.RowNumber() <= 100000)
                {
                    try
                    {
                        string productNumber = dataRow.Cell("A").Value.ToString().Trim();
                        string settingNumber = dataRow.Cell("B").Value.ToString().Trim();
                        string machineCategoryNumber = dataRow.Cell("C").Value.ToString().Trim();
                        string machineNumber = dataRow.Cell("D").Value.ToString().Trim();
                        string workCenterNumber = dataRow.Cell("E").Value.ToString().Trim();

                        _logger.LogInformation($"recno : {dataRow.RowNumber()}");


                        if (String.IsNullOrEmpty(productNumber))
                            break;


                        var product = await _context.Products.SingleOrDefaultAsync(x => x.ProductNumber.Trim() == productNumber);
                        
                        var setting = await _context.Settings
                            .Include(x=>x.MachineCategory)
                            .SingleOrDefaultAsync(x => x.SettingNumber.Trim() == settingNumber && x.MachineCategory.MachineCategoryNumber.Trim() == machineCategoryNumber);


                        var machineCategory = await _context.MachineCategories
                            .SingleOrDefaultAsync(x => x.MachineCategoryNumber == machineCategoryNumber);

                        var machine = await _context.Machines
                            .SingleOrDefaultAsync(x => x.MachineNumber == machineNumber);

                        var workCenter = await _context.Resources
                            .SingleOrDefaultAsync(x => x.ResourceNumber == workCenterNumber);


                        if (setting != null && product != null && machineCategory != null && machine != null && workCenter != null)
                        {

                            string sAltNo = dataRow.Cell("F").Value.ToString().Trim();
                            string sVersionNo = dataRow.Cell("G").Value.ToString().Trim();
                            string versionName = dataRow.Cell("H").Value.ToString().Trim();
                            string sMinValue = dataRow.Cell("I").Value.ToString().Trim();
                            string sDefValue = dataRow.Cell("J").Value.ToString().Trim();
                            string sMaxValue = dataRow.Cell("K").Value.ToString().Trim();
                            string text = dataRow.Cell("L").Value.ToString().Trim();


                            int versionNo = 1;
                            int altNo = 1;
                            decimal minValue = 0m;
                            decimal defValue = 0m;
                            decimal maxValue = 0m;



                            if (!String.IsNullOrEmpty(sVersionNo))
                            {
                                versionNo = Convert.ToInt32(sVersionNo);
                            }

                            if (!String.IsNullOrEmpty(sAltNo))
                            {
                                altNo = Convert.ToInt32(sAltNo);
                            }

                            if (!String.IsNullOrEmpty(sMinValue))
                                decimal.TryParse(sMinValue, out minValue);
                            
                            if (!String.IsNullOrEmpty(sDefValue))
                                decimal.TryParse(sDefValue, out defValue);


                            if (!String.IsNullOrEmpty(sMaxValue))
                                decimal.TryParse(sMaxValue, out maxValue);
                            




                            var settingFromExcelToImport = new SettingFromExcelToImport
                            {
                                AltNo = altNo,
                                VersionNo = versionNo,
                                VersionName = versionName,
                                SettingId = setting.Id,
                                ProductId = product.Id,
                                MachineCategoryId = machineCategory.Id,
                                MachineId = machine.Id,
                                WorkCenterId = workCenter.Id,
                                MinValue =  String.IsNullOrEmpty(sMinValue)?null:minValue,
                                Value = String.IsNullOrEmpty(sDefValue)?null:defValue,
                                MaxValue = String.IsNullOrEmpty(sMaxValue)?null:maxValue,
                                Text = text,
                            };

                            settingsToImport.Add(settingFromExcelToImport);


                        }
                        else
                        {

                            if (product == null)
                            {
                                _logger.LogInformation($"import settings error - brak indeksu: {productNumber}");
                            }
                            if (setting == null)
                            {
                                _logger.LogInformation($"import settings error - brak ustawienia: {settingNumber}");
                            }
                            if (machineCategory == null)
                            {
                                _logger.LogInformation($"import settings error - brak kategorii maszyny: {machineCategoryNumber}");
                            }
                            if (machine == null)
                            {
                                _logger.LogInformation($"import settings error - brak maszyny: {machineNumber}");
                            }



                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
        }


        return settingsToImport;
    }

}
