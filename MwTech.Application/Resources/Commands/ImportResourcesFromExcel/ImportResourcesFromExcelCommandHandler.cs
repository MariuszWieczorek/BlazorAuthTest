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

namespace MwTech.Application.Resources.Commands.ImportResourcesFromExcel;

public class ImportResourcesFromExcelCommandHandler : IRequestHandler<ImportResourcesFromExcelCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<ImportResourcesFromExcelCommandHandler> _logger;

    public ImportResourcesFromExcelCommandHandler(IApplicationDbContext context,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        ILogger<ImportResourcesFromExcelCommandHandler> logger
        )
    {
        _context = context;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }
    public async Task Handle(ImportResourcesFromExcelCommand request, CancellationToken cancellationToken)
    {

        string fileName = request.FileName;


        var resourcesToImport = await GetResourcesFromExcel(fileName);
        var currentUserId = _currentUserService.UserId;

        if (resourcesToImport.Count() == 0)
            throw new Exception("Brak rekordów do zaimportowania");



        foreach (var item in resourcesToImport)
        {


            var resource = _context.Resources
                        .SingleOrDefault(x => x.ResourceNumber == item.ResourceNumber);

            if (resource == null)
            {
                var newResource = new Resource()
                {
                    ResourceNumber = item.ResourceNumber,
                    Name = item.ResourceName,
                    ProductCategoryId = item.ProductCategoryId,
                    UnitId = item.UnitId,
                    Description = item.Description

            };

                _context.Resources.Add(newResource);
            }
            else
            {

                resource.Name = item.ResourceName;
                resource.ProductCategoryId = item.ProductCategoryId;
                resource.Description = item.Description;
                resource.UnitId = item.UnitId;
                _context.Resources.Update(resource);

            }

            await _context.SaveChangesAsync();



        }
        return;
    }


    private async Task<IEnumerable<ResourceToImport>> GetResourcesFromExcel(string fileName)
    {
        int licznik = 0;
        var resourcesToImport = new List<ResourceToImport>();

        using (var excelWorkbook = new XLWorkbook(fileName))
        {
            var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
            foreach (var dataRow in nonEmptyDataRows)
            {
                if (dataRow.RowNumber() >= 2)
                {
                    licznik++;
                    string resourceNumber = dataRow.Cell("B").Value.ToString().Trim();
                    string resourceName = dataRow.Cell( "C").Value.ToString().Trim();
                    string productCategoryNumber = dataRow.Cell("D").Value.ToString().Trim();
                    string unitCode = dataRow.Cell("E").Value.ToString().Trim();
                    string um = dataRow.Cell("A").Value.ToString().Trim();


                    if (string.IsNullOrWhiteSpace(resourceNumber))
                        break;


                    var unit = _context.Units
                        .SingleOrDefault(x => x.UnitCode.Trim() == unitCode.Trim());

                    var productCategory = _context.ProductCategories
                        .SingleOrDefault(x => x.CategoryNumber.Trim() == productCategoryNumber.Trim());

                    if (unit==null)
                    {
                        string message = $"Import Zasobów: brak jednostki {unitCode} !";
                        _logger.LogInformation(message);
                        throw new Exception(message);
                    }

                    if (productCategory == null)
                    {
                        string message = $"Import Zasobów: brak kategorii {productCategoryNumber} !";
                        _logger.LogInformation(message);
                        throw new Exception(message);
                    }

                    var resourceToImport = new ResourceToImport
                    {
                        ResourceNumber = resourceNumber,
                        ResourceName = resourceName,
                        ProductCategoryId = productCategory.Id,
                        UnitId = unit.Id,
                        Description = um,
                    };


                    resourcesToImport.Add(resourceToImport);

                }

            }
        }

        return resourcesToImport;
    }

}
