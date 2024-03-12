using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.Boms.Commands.ImportBomsFromExcel;

public class ImportBomsFromExcelCommandHandler : IRequestHandler<ImportBomsFromExcelCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ImportBomsFromExcelCommandHandler> _logger;

    public ImportBomsFromExcelCommandHandler(IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        ILogger<ImportBomsFromExcelCommandHandler> logger

        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }
    public async Task Handle(ImportBomsFromExcelCommand request, CancellationToken cancellationToken)
    {

        string fileName = request.FileName;

        if (!File.Exists(fileName))
        {
            throw new Exception($"Brak pliku {fileName}");
        }

        int counter = 1;

        var bomsToImport = await GetProductsStructuresFromExcel(fileName);

        var currentUserId = _currentUserService.UserId;

        if (bomsToImport.Count() == 0)
            throw new Exception("Brak struktur produktowych do zaimportowania");

        var sets = bomsToImport
            .GroupBy(x => new { x.SetId, x.AltNo, x.AltName, x.VersionNo, x.IsActive, x.IsDefault });


        foreach (var item in sets)
        {


            int setId = item.Key.SetId;

            ProductVersion productVersion = null;
           // bool defaultVersion = item.Key.VersionNo == 1;
            bool newVersion = false;



            productVersion = await _context.ProductVersions
                .SingleOrDefaultAsync(x =>
                x.ProductId == setId
                && x.AlternativeNo == item.Key.AltNo
                && x.VersionNumber == item.Key.VersionNo);


            newVersion = productVersion == null;


            if (newVersion)
            {
                productVersion = new ProductVersion
                {
                    ProductId = setId,
                    VersionNumber = item.Key.VersionNo,
                    AlternativeNo = item.Key.AltNo,
                    Name = item.Key.AltName,
                    CreatedByUserId = _currentUserService.UserId,
                    CreatedDate = _dateTimeService.Now,
                    DefaultVersion = item.Key.IsDefault,
                    IfsDefaultVersion = item.Key.IsDefault,
                    ToIfs = true,
                    IsActive = item.Key.IsActive,
                    ComarchDefaultVersion = false
                    
                };
            }
            else
            {
                productVersion.DefaultVersion = item.Key.IsDefault;
                productVersion.IsActive = item.Key.IsActive;
            }





            var boms = bomsToImport
             .Where(x => x.SetId == setId
                 && x.AltNo == item.Key.AltNo
                 && x.VersionNo == item.Key.VersionNo
                 )
             .GroupBy(x =>
             new
             {
                 x.AltNo,
                 x.VersionNo,
                 x.SetId,
                 x.PartId,
                 x.Qty,
                 x.OnProductionOrder,
                 x.No,
                 x.Excess
             });


            foreach (var bom in boms)
            {

            counter++;


                var bomToUpdate = await _context.Boms
                        .Include(x => x.SetVersion)
                        .Include(x => x.SetVersion.Product)
                        .SingleOrDefaultAsync(x =>
                           x.SetVersion.ProductId == bom.Key.SetId
                        && x.SetVersion.AlternativeNo == bom.Key.AltNo
                        && x.SetVersion.VersionNumber == bom.Key.VersionNo
                        && x.OrdinalNumber == bom.Key.No
                        );


                if (bomToUpdate == null)
                {
                    var productStructureToAdd = new Bom()
                    {
                        SetId = setId,
                        SetVersion = productVersion,
                        PartQty = bom.Key.Qty,
                        PartId = bom.Key.PartId,
                        Excess = bom.Key.Excess,
                        OrdinalNumber = bom.Key.No,
                        OnProductionOrder = bom.Key.OnProductionOrder,
                        DoNotIncludeInWeight = !bom.Key.OnProductionOrder,
                    };

                    _context.Boms.Add(productStructureToAdd);
                }
                else
                {
                    bomToUpdate.PartId = bom.Key.PartId;
                    bomToUpdate.PartQty = bom.Key.Qty;
                    bomToUpdate.Excess = bom.Key.Excess;
                    bomToUpdate.OrdinalNumber = bom.Key.No;
                    bomToUpdate.OnProductionOrder = bom.Key.OnProductionOrder;
                    bomToUpdate.DoNotIncludeInWeight = !bom.Key.OnProductionOrder;

                    productVersion.Name = item.Key.AltName;
                    productVersion.IsActive = item.Key.IsActive;
                    productVersion.DefaultVersion = item.Key.IsDefault;
                }

                
                await _context.SaveChangesAsync();

            }

        }
        return;
    }


    private async Task<IEnumerable<ProductStructureToImport>> GetProductsStructuresFromExcel(string fileName)
    {

        List<ProductStructureToImport> productsStructuresToImport = new List<ProductStructureToImport>();

        var units = await _context.Units.AsNoTracking().ToListAsync();
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
                        // A - Umiejscowienie
                        string setProductNumber = dataRow.Cell("B").Value.ToString().Trim();
                        string altName = dataRow.Cell("D").Value.ToString().Trim();
                        string sAltNo = dataRow.Cell("E").Value.ToString().Trim();
                        string sVersionNo = dataRow.Cell("F").Value.ToString().Trim();
                        string sIsActive = dataRow.Cell("G").Value.ToString().Trim();
                        string sNo = dataRow.Cell("H").Value.ToString().Trim();
                        string partProductNumber = dataRow.Cell("I").Value.ToString().Trim();
                        string sQty = dataRow.Cell("J").Value.ToString().Trim();
                        string sExcess = dataRow.Cell("K").Value.ToString().Trim();
                        string sOnProductionOrder = dataRow.Cell("L").Value.ToString().Trim();
                        string sLayer = dataRow.Cell("M").Value.ToString().Trim();
                        string sIsDef = dataRow.Cell("N").Value.ToString().Trim();

                        _logger.LogInformation($"recno : {dataRow.RowNumber()}");


                        if (String.IsNullOrEmpty(partProductNumber))
                            break;


                        var setProduct = await _context.Products.SingleOrDefaultAsync(x => x.ProductNumber == setProductNumber);
                        var partProduct = await _context.Products.SingleOrDefaultAsync(x => x.ProductNumber == partProductNumber);

                        if (setProduct != null && partProduct != null)
                        {

                            
                            
                            
                            



                            int no = 1;
                            int versionNo = 1;
                            int altNo = 1;
                            int layer = 0;
                            decimal qty = 0m;
                            decimal excess = 0m;
                            bool onProductionOrder = false;
                            bool isActive = true;
                            bool isDef = true;

                            if (!String.IsNullOrEmpty(sQty))
                                qty = Convert.ToDecimal(sQty);

                            if (!String.IsNullOrEmpty(sVersionNo))
                            {
                                if (sVersionNo == "*")
                                    versionNo = 1;
                                else
                                    versionNo = Convert.ToInt32(sVersionNo);
                            }

                            if (!String.IsNullOrEmpty(sQty))
                                decimal.TryParse(sQty, out qty);


                            if (!String.IsNullOrEmpty(sExcess))
                                excess = Convert.ToDecimal(sExcess);

                            if (!String.IsNullOrEmpty(sNo))
                                no = Convert.ToInt32(sNo);

                            if (!String.IsNullOrEmpty(sOnProductionOrder))
                            {

                                onProductionOrder = (sOnProductionOrder == "1") || (sOnProductionOrder == "Zużywana") || (sOnProductionOrder == "Consumed") || (sOnProductionOrder == "TRUE");
                            }

                            if (!String.IsNullOrEmpty(sLayer))
                                layer = Convert.ToInt32(sNo);

                            if (!String.IsNullOrEmpty(sAltNo))
                                altNo = Convert.ToInt32(sAltNo);

                            if (!String.IsNullOrEmpty(sIsActive))
                            {

                                isActive = (sIsActive == "1") || (sIsActive == "TRUE") || (sIsActive == "T");
                            }

                            if (!String.IsNullOrEmpty(sIsDef))
                            {

                                isDef = (sIsDef == "1") || (sIsDef == "TRUE") || (sIsDef == "T");
                            }


                            var productStructureToImport = new ProductStructureToImport
                            {
                                AltNo = altNo,
                                AltName = altName,
                                VersionNo = versionNo,
                                IsActive= isActive,
                                SetId = setProduct.Id,
                                PartId = partProduct.Id,
                                No = no,
                                Qty = qty,
                                Excess = excess,
                                OnProductionOrder = onProductionOrder,
                                Layer = layer,
                                IsDefault = isDef
                            };

                            productsStructuresToImport.Add(productStructureToImport);


                        }
                        else
                        {
                            _logger.LogInformation($"import structures error : {setProductNumber} => {partProductNumber}");
                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
        }


        return productsStructuresToImport;
    }

}
