using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.Routes.Commands.ImportRoutesFromExcel;

public class ImportRoutesFromExcelCommandHandler : IRequestHandler<ImportRoutesFromExcelCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ImportRoutesFromExcelCommandHandler> _logger;

    public ImportRoutesFromExcelCommandHandler(IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        ILogger<ImportRoutesFromExcelCommandHandler> logger

        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }
    public async Task Handle(ImportRoutesFromExcelCommand request, CancellationToken cancellationToken)
    {

        string fileName = request.FileName;

        if (!File.Exists(fileName))
        {
            throw new Exception($"Brak pliku {fileName}");
        }


        int counter = 1;

        var routesToImport = await GetRoutesFromExcel(fileName);


        var currentUserId = _currentUserService.UserId;

        if (routesToImport.Count() == 0)
            throw new Exception("Brak marszrut do zaimportowania");

        var headers = routesToImport
            .GroupBy(x => new { x.ProductId, x.RouteAltNo, x.RouteAltName, x.RouteVersionNo, x.IsActive, x.IsDefault, x.ProductCategoryId });



        foreach (var item in headers)
        {
            int productId = item.Key.ProductId;

            RouteVersion routeVersion = null;
            bool defaultVersion = item.Key.IsDefault;
            bool newVersion = false;



            routeVersion = await _context.RouteVersions
                .SingleOrDefaultAsync(x =>
                x.ProductId == productId
                && x.AlternativeNo == item.Key.RouteAltNo
                && x.VersionNumber == item.Key.RouteVersionNo
                && x.ProductCategoryId == item.Key.ProductCategoryId
                );


            newVersion = routeVersion == null;


            if (newVersion)
            {
                routeVersion = new RouteVersion
                {
                    ProductId = productId,
                    AlternativeNo = item.Key.RouteAltNo,
                    VersionNumber = item.Key.RouteVersionNo,
                    ProductCategoryId = item.Key.ProductCategoryId,
                    Name = item.Key.RouteAltName,
                    CreatedByUserId = _currentUserService.UserId,
                    CreatedDate = _dateTimeService.Now,
                    DefaultVersion = defaultVersion,
                    IfsDefaultVersion = defaultVersion,
                    ToIfs = true,
                    IsActive = item.Key.IsActive,
                    ComarchDefaultVersion = false
                };
            }
            else
            {
                routeVersion.DefaultVersion = defaultVersion;
                routeVersion.IsActive = item.Key.IsActive;
            }



            if (routeVersion != null)
            {

                // Pobieram pozycje i je grupuję aby uniknąć duplikatów
                var routes = routesToImport
                 .Where(x => x.ProductId == productId
                 && x.RouteAltNo == item.Key.RouteAltNo
                 && x.RouteVersionNo == item.Key.RouteVersionNo
                 && x.ProductCategoryId == item.Key.ProductCategoryId
                 )
                 .GroupBy(x => new
                 {
                     x.ProductId,
                     x.RouteAltNo,
                     x.RouteVersionNo,
                     x.No,
                     x.OperationId,
                     x.WorkCenterId,
                     x.ResourceId,
                     x.ChangeOverResourceId,
                     x.ResourceQty,
                     x.OperationLabourConsumption,
                     x.OperationMachineConsumption,
                     x.ChangeOverNumberOfEmployee,
                     x.ChangeOverLabourConsumption,
                     x.ChangeOverMachineConsumption,
                     x.MoveTime,
                     x.OverLap,
                     x.ProductCategoryId
                 });

                // todo dodać narzędzia do importu

                foreach (var singleRoute in routes)
                {

                    counter++;


                    var routePositionToUpdate = await _context.ManufactoringRoutes
                        .Include(x => x.RouteVersion)
                        .Include(x => x.RouteVersion.Product)
                        .SingleOrDefaultAsync(x =>
                           x.RouteVersion.ProductId == singleRoute.Key.ProductId
                        && x.RouteVersion.AlternativeNo == singleRoute.Key.RouteAltNo
                        && x.RouteVersion.VersionNumber == singleRoute.Key.RouteVersionNo
                        && x.RouteVersion.ProductCategoryId == singleRoute.Key.ProductCategoryId
                        && x.OrdinalNumber == singleRoute.Key.No
                        );

                    if (routePositionToUpdate == null)
                    {
                        var routeToAdd = new ManufactoringRoute()
                        {
                            RouteVersion = routeVersion,
                            ResourceId = singleRoute.Key.ResourceId,
                            OperationId = singleRoute.Key.OperationId,
                            WorkCenterId = singleRoute.Key.WorkCenterId,
                            ChangeOverResourceId = singleRoute.Key.ChangeOverResourceId,
                            OrdinalNumber = singleRoute.Key.No,
                            OperationLabourConsumption = singleRoute.Key.OperationLabourConsumption,
                            OperationMachineConsumption = singleRoute.Key.OperationMachineConsumption,
                            ResourceQty = singleRoute.Key.ResourceQty,
                            ChangeOverLabourConsumption = singleRoute.Key.ChangeOverLabourConsumption,
                            ChangeOverMachineConsumption = singleRoute.Key.ChangeOverMachineConsumption,
                            ChangeOverNumberOfEmployee = singleRoute.Key.ChangeOverNumberOfEmployee,
                            MoveTime = singleRoute.Key.ChangeOverNumberOfEmployee,
                            Overlap = singleRoute.Key.OverLap
                        };

                        _context.ManufactoringRoutes.Add(routeToAdd);
                    }
                    else
                    {
                        routePositionToUpdate.WorkCenterId = singleRoute.Key.WorkCenterId;
                        routePositionToUpdate.OperationId = singleRoute.Key.OperationId;
                        routePositionToUpdate.ResourceId = singleRoute.Key.ResourceId;
                        routePositionToUpdate.ChangeOverResourceId = singleRoute.Key.ChangeOverResourceId;
                        routePositionToUpdate.OrdinalNumber = singleRoute.Key.No;
                        routePositionToUpdate.OperationLabourConsumption = singleRoute.Key.OperationLabourConsumption;
                        routePositionToUpdate.OperationMachineConsumption = singleRoute.Key.OperationMachineConsumption;
                        routePositionToUpdate.ResourceQty = singleRoute.Key.ResourceQty;
                        routePositionToUpdate.ChangeOverLabourConsumption = singleRoute.Key.ChangeOverLabourConsumption;
                        routePositionToUpdate.ChangeOverMachineConsumption = singleRoute.Key.ChangeOverMachineConsumption;
                        routePositionToUpdate.ChangeOverNumberOfEmployee = singleRoute.Key.ChangeOverNumberOfEmployee;
                        routePositionToUpdate.MoveTime = singleRoute.Key.MoveTime;
                        routePositionToUpdate.Overlap = singleRoute.Key.OverLap;

                        // routeVersion.Name = _context.Resources.SingleOrDefault(x => x.Id == singleRoute.Key.WorkCenterId)?.ResourceNumber ?? "x";
                        routeVersion.Name = item.Key.RouteAltName;
                        routeVersion.IsActive = item.Key.IsActive;
                    }

                    await _context.SaveChangesAsync();
                }
            }



                _context.Clear();

        }


        return;
    }


    private async Task<IEnumerable<RouteToImport>> GetRoutesFromExcel(string fileName)
    {


        var routesToImport = new List<RouteToImport>();

        int counter = 1;

        using (var excelWorkbook = new XLWorkbook(fileName))
        {
            var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();



            foreach (var dataRow in nonEmptyDataRows)
            {
                counter++;

                if (dataRow.RowNumber() >= 2 && dataRow.RowNumber() <= 100000)
                {
                    //to get column # 3's data
                    try
                    {









                        // A - Unmiejscowienie
                        //
                        // B - Kategoria Produktu 
                        //
                        // C - Indeks Produktu
                        string productNumber = dataRow.Cell("C").Value.ToString().Trim();
                        // D - Numer wariantu Tyko IFS (* zamiast 1)                                
                        //
                        // E - Opis Wariantu
                        string sRouteAltName = dataRow.Cell("E").Value.ToString().Trim();
                        
                        // F - Numer Wersji
                        string sRouteVersionNo = dataRow.Cell("F").Value.ToString().Trim();
                        
                        // G - Numer Wariantu
                        string sRouteAltNo = dataRow.Cell("G").Value.ToString().Trim();
                        
                        // H - Czy Aktywny
                        string sIsActive = dataRow.Cell("H").Value.ToString().Trim();
                        // I - czy Domyślny
                        string sIsDefault = dataRow.Cell("I").Value.ToString().Trim();
                        // J - Kategoria Produktu w nagłóku Maraszruty
                        string sProductCategory = dataRow.Cell("J").Value.ToString().Trim();
                        // K - Numer Operacji
                        string sNo = dataRow.Cell("K").Value.ToString().Trim();
                        // L - Symbol/Opis Operacji 
                        string operationNumber = dataRow.Cell("L").Value.ToString().Trim();
                        // M - Symbol Gniazda Produkcyjnego
                        string workCenterNumber = dataRow.Cell("M").Value.ToString().Trim();
                        // N - Maszynochłonność Przebrojenia
                        string sChangeOverMachineConsumption = dataRow.Cell("N").Value.ToString().Trim();
                        // O - Pracochłonność Przebrojenia
                        string sChangeOverLabourConsumption = dataRow.Cell("O").Value.ToString().Trim();
                        // P - Kategoria zaszeregowania do przezbrojenia
                        string changeOverResourceNumber = dataRow.Cell("P").Value.ToString().Trim();
                        // Q - Wielkość Brygady do PRzezbrojenia
                        string sChangeOverNumberOfEmployee = dataRow.Cell("Q").Value.ToString().Trim();
                        // R - Maszynochłonność Operacji
                        string sOperationMachineConsumption = dataRow.Cell("R").Value.ToString().Trim();
                        // S - Pracochłonność Operacji
                        string sOperationLabourConsumption = dataRow.Cell("S").Value.ToString().Trim();
                        // T - Jednostka Miary dla Pracochłonności i Maszynochłonności Godz./jedn. lub Jedn./godz.
                        //
                        // U - Kategoria Zaszeregowania dla Operacji
                        string resourceNumber = dataRow.Cell("U").Value.ToString().Trim();
                        // V - Wielkość Brygady dla Operacji
                        string sResourceQty = dataRow.Cell("V").Value.ToString().Trim();
                        // W - Czas Transoprtu
                        string sMoveTime = dataRow.Cell("W").Value.ToString().Trim();
                        // X - Zachodzenie
                        string sOverLap = dataRow.Cell("X").Value.ToString().Trim();
                        
                        


                        if (string.IsNullOrWhiteSpace(productNumber))
                            break;

                        var product = await _context.Products.SingleOrDefaultAsync(x => x.ProductNumber == productNumber);
                        var operation = await _context.Operations.SingleOrDefaultAsync(x => x.OperationNumber == operationNumber);
                        var resource = await _context.Resources.SingleOrDefaultAsync(x => x.ResourceNumber == resourceNumber);
                        var workcenter = await _context.Resources.SingleOrDefaultAsync(x => x.ResourceNumber == workCenterNumber);
                        var changeOverResource = await _context.Resources.SingleOrDefaultAsync(x => x.ResourceNumber == changeOverResourceNumber);
                        var productCategory = await _context.ProductCategories.SingleOrDefaultAsync(x => x.CategoryNumber == sProductCategory);

                        if (product != null && operation != null && resource != null && workcenter != null)
                        {


                            int no = 1;
                            int routeAltNo = 1;
                            int routeVersionNo = 1;
                            decimal resourceQty = 0m;
                            decimal operationLabourConsumption = 0m;
                            decimal operationMachineConsumption = 0m;


                            decimal changeOverNumberOfEmployee = 0m;
                            decimal changeOverLabourConsumption = 0m;
                            decimal changeOverMachineConsumption = 0m;
                            decimal moveTime = 0m;
                            decimal overLap = 0m;
                            bool isActive = true;
                            bool isDefault = true;


                            if (!String.IsNullOrEmpty(sNo))
                                no = Convert.ToInt32(sNo);


                            if (!String.IsNullOrEmpty(sRouteAltNo))
                            {

                                routeAltNo = Convert.ToInt32(sRouteAltNo);
                            }

                            if (!String.IsNullOrEmpty(sRouteVersionNo))
                            {

                                routeVersionNo = Convert.ToInt32(sRouteVersionNo);
                            }

                            if (!String.IsNullOrEmpty(sResourceQty))
                                resourceQty = Convert.ToDecimal(sResourceQty);


                            if (!String.IsNullOrEmpty(sOperationLabourConsumption))
                                operationLabourConsumption = Convert.ToDecimal(sOperationLabourConsumption);

                            if (!String.IsNullOrEmpty(sOperationMachineConsumption))
                                operationMachineConsumption = Convert.ToDecimal(sOperationMachineConsumption);


                            if (!String.IsNullOrEmpty(sChangeOverNumberOfEmployee))
                                changeOverNumberOfEmployee = Convert.ToDecimal(sChangeOverNumberOfEmployee);


                            if (!String.IsNullOrEmpty(sChangeOverLabourConsumption))
                                changeOverLabourConsumption = Convert.ToDecimal(sChangeOverLabourConsumption);

                            if (!String.IsNullOrEmpty(sChangeOverMachineConsumption))
                                changeOverMachineConsumption = Convert.ToDecimal(sChangeOverMachineConsumption);

                            if (!String.IsNullOrEmpty(sMoveTime))
                                moveTime = Convert.ToDecimal(sMoveTime);

                            if (!String.IsNullOrEmpty(sOverLap))
                                overLap = Convert.ToDecimal(sOverLap);

                            if (!String.IsNullOrEmpty(sIsActive))
                            {

                                isActive = (sIsActive == "1");
                            }

                            if (!String.IsNullOrEmpty(sIsDefault))
                            {

                                isDefault = (sIsDefault == "1");
                            }

                            var routeToImport = new RouteToImport
                            {
                                RouteAltNo = routeAltNo,
                                RouteAltName = sRouteAltName,
                                RouteVersionNo = routeVersionNo,

                                ProductId = product.Id,
                                OperationId = operation.Id,
                                ResourceId = resource.Id,
                                WorkCenterId = workcenter.Id,
                                ChangeOverResourceId = changeOverResource?.Id,

                                No = no,
                                ResourceQty = resourceQty,
                                OperationLabourConsumption = operationLabourConsumption,
                                OperationMachineConsumption = operationMachineConsumption,

                                ChangeOverNumberOfEmployee = changeOverNumberOfEmployee,
                                ChangeOverLabourConsumption = changeOverLabourConsumption,
                                ChangeOverMachineConsumption = changeOverMachineConsumption,

                                MoveTime = moveTime,
                                OverLap = overLap,

                                IsActive = isActive,
                                IsDefault = isDefault,
                                ProductCategoryId = productCategory?.Id
                            };

                            routesToImport.Add(routeToImport);


                        }
                        else
                        {

                            if (product == null && operation != null && resource != null && workcenter != null)
                                _logger.LogInformation($"Operation Import Error  wiersz {dataRow.RowNumber()}  Brak Produktu: {productNumber} ");
                            if (operation == null && resource != null && workcenter != null)
                                _logger.LogInformation($"Operation Import Error  wiersz {dataRow.RowNumber()}  Brak operacji {operationNumber}");
                            if (resource == null && workcenter != null)
                                _logger.LogInformation($"Operation Import Error  wiersz {dataRow.RowNumber()}  Brak zasobu {resourceNumber}");
                            if (workcenter == null)
                                _logger.LogInformation($"Operation Import Error  wiersz {dataRow.RowNumber()}  Brak gniazda {workCenterNumber}");
                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }


                }
            }



            return routesToImport;
        }

    }

}