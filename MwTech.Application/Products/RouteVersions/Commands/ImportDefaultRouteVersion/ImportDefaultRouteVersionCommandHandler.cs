using ClosedXML.Excel;
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

namespace MwTech.Application.Products.RouteVersions.Commands.ImportDefaultRouteVersion;

public class ImportDefaultRouteVersionCommandHandler : IRequestHandler<ImportDefaultRouteVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ImportDefaultRouteVersionCommandHandler> _logger;

    public ImportDefaultRouteVersionCommandHandler(IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        ILogger<ImportDefaultRouteVersionCommandHandler> logger

        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }
    public async Task Handle(ImportDefaultRouteVersionCommand request, CancellationToken cancellationToken)
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
            throw new Exception("Brak marszrut do ustawienia");

        var headers = routesToImport
            .Where(x => x.IsDefault && x.IsActive)
            .GroupBy(x => new { x.ProductId, x.RouteAltNo, x.RouteVersionNo});



        foreach (var item in headers)
        {
            int productId = item.Key.ProductId;

            int routeVersionNo = item.Key.RouteVersionNo;
            int routeAltNo = item.Key.RouteAltNo;




            var routeVersion = await _context.RouteVersions
                .SingleOrDefaultAsync(x =>
                x.ProductId == productId
                && x.AlternativeNo == routeAltNo
                && x.VersionNumber == routeVersionNo);



            if (routeVersion != null)
            {
                var routeVersions = _context.RouteVersions
                .Where(x => x.ProductId == productId);

                foreach (var routeVersionToUpdate in routeVersions)
                {
                    routeVersionToUpdate.DefaultVersion = (routeVersionToUpdate.Id == routeVersion.Id);
                }

                await _context.SaveChangesAsync();
            }
        }


        return;
    }


    private async Task<IEnumerable<RouteVersionToSetAsDefault>> GetRoutesFromExcel(string fileName)
    {


        var routesToImport = new List<RouteVersionToSetAsDefault>();

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
                        string productNumber = dataRow.Cell("B").Value.ToString().Trim();
                        string sIsDefault = dataRow.Cell("C").Value.ToString().Trim();
                        string sRouteAltName = dataRow.Cell("D").Value.ToString().Trim();
                        string sRouteAltNo = dataRow.Cell("E").Value.ToString().Trim();
                        string sRouteVersionNo = dataRow.Cell("F").Value.ToString().Trim();
                        string sIsActive = dataRow.Cell("G").Value.ToString().Trim();




                        if (string.IsNullOrWhiteSpace(productNumber))
                            break;

                        var product = await _context.Products.SingleOrDefaultAsync(x => x.ProductNumber == productNumber);


                        if (product != null)
                        {


                            int no = 1;
                            int routeAltNo = 1;
                            int routeVersionNo = 1;
                            bool isActive = true;
                            bool isDefault = true;



                            if (!String.IsNullOrEmpty(sRouteAltNo))
                            {

                                routeAltNo = Convert.ToInt32(sRouteAltNo);
                            }

                            if (!String.IsNullOrEmpty(sRouteVersionNo))
                            {

                                routeVersionNo = Convert.ToInt32(sRouteVersionNo);
                            }

                          
                            if (!String.IsNullOrEmpty(sIsActive))
                            {

                                isActive = (sIsActive == "1");
                            }



                            if (!String.IsNullOrEmpty(sIsDefault))
                            {

                                isDefault = (sIsDefault == "1" || sIsDefault == "*");
                            }


                            var routeVersionToSetAsDefault = new RouteVersionToSetAsDefault
                            {
                                ProductId = product.Id,
                                RouteAltNo = routeAltNo,
                                RouteVersionNo = routeVersionNo,
                                IsActive = isActive,
                                IsDefault = isDefault
                            };

                            routesToImport.Add(routeVersionToSetAsDefault);


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