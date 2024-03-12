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

namespace MwTech.Application.Products.ProductVersions.Commands.ImportDefaultProductVersion;

public class ImportDefaultProductVersionCommandHandler : IRequestHandler<ImportDefaultProductVersionCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ImportDefaultProductVersionCommandHandler> _logger;

    public ImportDefaultProductVersionCommandHandler(IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        ILogger<ImportDefaultProductVersionCommandHandler> logger

        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }
    public async Task Handle(ImportDefaultProductVersionCommand request, CancellationToken cancellationToken)
    {

        string fileName = request.FileName;

        if (!File.Exists(fileName))
        {
            throw new Exception($"Brak pliku {fileName}");
        }


        int counter = 1;

        var bomsToSet = await GetProductsFromExcel(fileName);


        var currentUserId = _currentUserService.UserId;

        if (bomsToSet.Count() == 0)
            throw new Exception("Brak bomów do ustawienia");

        var headers = bomsToSet
            .GroupBy(x => new { x.ProductId, x.ProductAltNo, x.ProductVersionNo, x.IsDefault, x.IsActive});



        foreach (var item in headers)
        {
            int productId = item.Key.ProductId;

            int versionNo = item.Key.ProductVersionNo;
            int altNo = item.Key.ProductAltNo;




            var productVersion = await _context.ProductVersions
                .SingleOrDefaultAsync(x =>
                x.ProductId == productId
                && x.AlternativeNo == altNo
                && x.VersionNumber == versionNo);


            // ustawienie wersji jako domyślnej i pozostałych jako niedomyslne
            if (productVersion != null && item.Key.IsDefault)
            {
                var productVersions = _context.ProductVersions
                .Where(x => x.ProductId == productId);

                foreach (var productVersionToUpdate in productVersions)
                {
                    productVersionToUpdate.DefaultVersion = (productVersionToUpdate.Id == productVersion.Id);
                    productVersionToUpdate.IsActive = productVersionToUpdate.DefaultVersion;
                }

                await _context.SaveChangesAsync();
            }
        }


        return;
    }


    private async Task<IEnumerable<ProductVersionToSetAsDefault>> GetProductsFromExcel(string fileName)
    {


        var routesToImport = new List<ProductVersionToSetAsDefault>();

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
                        string sProductAltName = dataRow.Cell("D").Value.ToString().Trim();
                        string sProductAltNo = dataRow.Cell("E").Value.ToString().Trim();
                        string sProductVersionNo = dataRow.Cell("F").Value.ToString().Trim();
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



                            if (!String.IsNullOrEmpty(sProductAltNo))
                            {

                                routeAltNo = Convert.ToInt32(sProductAltNo);
                            }

                            if (!String.IsNullOrEmpty(sProductVersionNo))
                            {

                                routeVersionNo = Convert.ToInt32(sProductVersionNo);
                            }

                          
                            if (!String.IsNullOrEmpty(sIsActive))
                            {

                                isActive = (sIsActive == "1");
                            }



                            if (!String.IsNullOrEmpty(sIsDefault))
                            {

                                isDefault = (sIsDefault == "1" || sIsDefault == "*");
                            }


                            var routeVersionToSetAsDefault = new ProductVersionToSetAsDefault
                            {
                                ProductId = product.Id,
                                ProductAltNo = routeAltNo,
                                ProductVersionNo = routeVersionNo,
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