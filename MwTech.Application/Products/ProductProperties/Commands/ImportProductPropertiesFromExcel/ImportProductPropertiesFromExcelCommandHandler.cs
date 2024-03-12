using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.ProductProperties.Commands.ImportProductPropertiesFromExcel;

public class ImportProductPropertiesFromExcelCommandHandler : IRequestHandler<ImportProductPropertiesFromExcelCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ImportProductPropertiesFromExcelCommandHandler> _logger;

    public ImportProductPropertiesFromExcelCommandHandler(IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        ILogger<ImportProductPropertiesFromExcelCommandHandler> logger

        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }
    public async Task Handle(ImportProductPropertiesFromExcelCommand request, CancellationToken cancellationToken)
    {

        string fileName = request.FileName;

        if (!File.Exists(fileName))
        {
            throw new Exception($"Brak pliku {fileName}");
        }

        int counter = 1;

        var propertiesToImport = await GetProductPropertiesFromExcel(fileName);

        var currentUserId = _currentUserService.UserId;

        if (propertiesToImport.Count() == 0)
            throw new Exception("Brak atrybutów do zaimportowania");


        var versions = propertiesToImport
            .GroupBy(x => new { x.ProductId, x.VersionNo, x.AltNo, x.VersionName, x.IsDefault, x.IsActive });


        foreach (var item in versions)
        {



            ProductPropertyVersion productPropertyVersion = null;
            bool newVersion = false;



            productPropertyVersion = await _context.ProductPropertyVersions
              .SingleOrDefaultAsync(x => 
                 x.ProductId == item.Key.ProductId
              && x.AlternativeNo == item.Key.AltNo
              && x.VersionNumber == item.Key.VersionNo);

            newVersion = productPropertyVersion == null;


            if (newVersion)
            {
                productPropertyVersion = new ProductPropertyVersion
                {
                    AlternativeNo = item.Key.AltNo,
                    VersionNumber = item.Key.VersionNo,
                    Name = item.Key.VersionName,
                    ProductId = item.Key.ProductId,
                    CreatedByUserId = _currentUserService.UserId,
                    CreatedDate = _dateTimeService.Now,
                    DefaultVersion = item.Key.IsDefault,
                    IsActive = item.Key.IsActive,
                };
            }
            else
            {
                productPropertyVersion.DefaultVersion = item.Key.IsDefault && item.Key.IsActive;
                productPropertyVersion.IsActive = item.Key.IsActive;
                productPropertyVersion.Name = item.Key.VersionName;
            }



            var productProperties = propertiesToImport
             .Where(x => x.ProductId == item.Key.ProductId
             && x.AltNo == item.Key.AltNo
             && x.VersionNo == item.Key.VersionNo);


            foreach (var productProperty in productProperties)
            {

                counter++;

                var productPropertyToUpdate = await _context.ProductProperties
                    .Include(x => x.ProductPropertiesVersion)
                    .SingleOrDefaultAsync(x =>
                       x.ProductPropertiesVersion.ProductId == productProperty.ProductId
                    && x.ProductPropertiesVersion.VersionNumber == productProperty.VersionNo
                    && x.ProductPropertiesVersion.AlternativeNo == productProperty.AltNo
                    && x.PropertyId == productProperty.PropertyId
                    );


                if (productPropertyToUpdate == null)
                {
                    var productPropertyToAdd = new ProductProperty()
                    {
                        ProductPropertiesVersion = productPropertyVersion,
                        PropertyId = productProperty.PropertyId,
                        MinValue = productProperty.MinValue,
                        Value = productProperty.Value,
                        MaxValue = productProperty.MaxValue,
                        Text = productProperty.Text
                    };

                    _context.ProductProperties.Add(productPropertyToAdd);
                }
                else
                {
                    productPropertyToUpdate.MinValue = productProperty.MinValue;
                    productPropertyToUpdate.Value = productProperty.Value;
                    productPropertyToUpdate.MaxValue = productProperty.MaxValue;
                    productPropertyToUpdate.Text = productProperty.Text;
                }


                await _context.SaveChangesAsync();

            }

        }
        return;
    }


    private async Task<IEnumerable<ProductPropertyToImport>> GetProductPropertiesFromExcel(string fileName)
    {

        List<ProductPropertyToImport> propertiesToImport = new List<ProductPropertyToImport>();

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
                        string productCategoryNumber = dataRow.Cell("A").Value.ToString().Trim();
                        string categoryName = dataRow.Cell("B").Value.ToString().Trim();
                        string productNumber = dataRow.Cell("C").Value.ToString().Trim();
                        string productName = dataRow.Cell("D").Value.ToString().Trim();
                        string propertyNumber = dataRow.Cell("E").Value.ToString().Trim();
                        string propertyName = dataRow.Cell("F").Value.ToString().Trim();

                        _logger.LogInformation($"recno : {dataRow.RowNumber()}");


                        if (String.IsNullOrEmpty(productNumber))
                            break;


                        var product = await _context.Products.SingleOrDefaultAsync(x => x.ProductNumber.Trim() == productNumber);
                        var property = await _context.Properties.SingleOrDefaultAsync(x => x.PropertyNumber.Trim() == propertyNumber);
                        var productCategory = await _context.ProductCategories
                            .SingleOrDefaultAsync(x => x.CategoryNumber == productCategoryNumber);



                        if (property != null && product != null && productCategory != null)
                        {

                            
                            string sVersionNo = dataRow.Cell("G").Value.ToString().Trim();
                            string sAltNo = dataRow.Cell("H").Value.ToString().Trim();
                            string versionName = dataRow.Cell("I").Value.ToString().Trim();
                            string sIsDefault = dataRow.Cell("J").Value.ToString().Trim();
                            string sIsActive = dataRow.Cell("K").Value.ToString().Trim();
                            
                            string text = dataRow.Cell("L").Value.ToString().Trim();
                            string sMinValue = dataRow.Cell("M").Value.ToString().Trim();
                            string sDefValue = dataRow.Cell("N").Value.ToString().Trim();
                            string sMaxValue = dataRow.Cell("O").Value.ToString().Trim();
                            


                            int versionNo = 1;
                            int altNo = 1;
                            decimal minValue = 0m;
                            decimal defValue = 0m;
                            decimal maxValue = 0m;
                            bool isActive = false;
                            bool isDefault = false;



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

                            if (!String.IsNullOrEmpty(sIsActive))
                            {

                                isActive = (sIsActive == "1") || (sIsActive == "TRUE") || (sIsActive == "T");
                            }

                            if (!String.IsNullOrEmpty(sIsDefault))
                            {

                                isDefault = (sIsDefault == "1") || (sIsDefault == "TRUE") || (sIsDefault == "T");
                            }



                            var propertyFromExcelToImport = new ProductPropertyToImport
                            {
                                AltNo = altNo,
                                VersionNo = versionNo,
                                VersionName = versionName,
                                IsActive = isActive,
                                IsDefault = isDefault,
                                PropertyId = property.Id,
                                ProductId = product.Id,
                                ProductCategoryId = productCategory.Id,
                                MinValue =  String.IsNullOrEmpty(sMinValue)?null:minValue,
                                Value = String.IsNullOrEmpty(sDefValue)?null:defValue,
                                MaxValue = String.IsNullOrEmpty(sMaxValue)?null:maxValue,
                                Text = text,
                            };

                            propertiesToImport.Add(propertyFromExcelToImport);


                        }
                        else
                        {

                            if (product == null)
                            {
                                _logger.LogInformation($"import settings error - brak indeksu: {productNumber}");
                            }
                            if (property == null)
                            {
                                _logger.LogInformation($"import settings error - brak ustawienia: {propertyNumber}");
                            }
                            if (productCategory == null)
                            {
                                _logger.LogInformation($"import settings error - brak kategorii maszyny: {productCategoryNumber}");
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


        return propertiesToImport;
    }

}


