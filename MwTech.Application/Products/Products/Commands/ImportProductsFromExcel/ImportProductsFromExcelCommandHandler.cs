using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Products.Commands.ImportProductsFromExcel;

public class ImportProductsFromExcelCommandHandler : IRequestHandler<ImportProductsFromExcelCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IComarchService _comarchService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IProductCostService _productCostService;

    public ImportProductsFromExcelCommandHandler(IApplicationDbContext context,
        IComarchService comarchService,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService,
        IProductCostService productCostService
        )
    {
        _context = context;
        _comarchService = comarchService;
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
        _productCostService = productCostService;
    }
    public async Task Handle(ImportProductsFromExcelCommand request, CancellationToken cancellationToken)
    {

        string fileName = request.FileName;


        var productsToImport = await GetProductsFromExcel(fileName);
        var currentUserId = _currentUserService.UserId;

        if (productsToImport.Count() == 0)
            throw new Exception("Brak produktów do zaimportowania");


       
        foreach (var item in productsToImport)
        {


            var productSavedInDb = _context.Products
                        .SingleOrDefault(x => x.ProductNumber == item.ProductNumber);

            if (productSavedInDb == null)
            {
                var productToAdd = new Product()
                {
                    ProductNumber = item.ProductNumber,
                    OldProductNumber = item.OldProductNumber,
                    Idx01 = item.Idx01,
                    Idx02 = item.Idx02,
                    Name = item.Name,
                    UnitId = item.UnitId,
                    ProductCategoryId = item.ProductCategoryId,
                    CreatedByUserId = currentUserId,
                    CreatedDate = _dateTimeService.Now,
                    IsActive = true
                };
                _context.Products.Add(productToAdd);
            }
            else
            {
                productSavedInDb.OldProductNumber = item.OldProductNumber;
                productSavedInDb.Idx01 = item.Idx01;
                productSavedInDb.Idx02 = item.Idx02;
                productSavedInDb.Name = item.Name;
                productSavedInDb.UnitId = item.UnitId;
                productSavedInDb.ProductCategoryId = item.ProductCategoryId;

            }
            
            await _context.SaveChangesAsync();
            _context.Clear();
        }
        return;
    }


    private async Task<IEnumerable<ProductToImport>> GetProductsFromExcel(string fileName)
    {

        var productsToImport = new List<ProductToImport>();
        var units = await _context.Units.AsNoTracking().ToListAsync();
        var productCategories = await _context.ProductCategories.AsNoTracking().ToListAsync();

        using (var excelWorkbook = new XLWorkbook(fileName))
        {
            var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();




            foreach (var dataRow in nonEmptyDataRows)
            {
                if (dataRow.RowNumber() >= 2 && dataRow.RowNumber() <= 100000)
                {
                    try
                    {
                        string productNumber = dataRow.Cell("A").Value.ToString().Trim();

                        var productInDb = _context.Products
                        .Where(x => x.ProductNumber.Trim() == productNumber.Trim()).Any();

                       

                            string productName = dataRow.Cell("B").Value.ToString().Trim();
                            string unitCode = dataRow.Cell("C").Value.ToString().Trim();
                            string categoryNumber = dataRow.Cell("D").Value.ToString().Trim();
                            string oldProductNumber = dataRow.Cell("E").Value.ToString().Trim();
                            string idx01 = dataRow.Cell("F").Value.ToString().Trim();
                            string idx02 = dataRow.Cell("G").Value.ToString().Trim();




                            var unit = units.SingleOrDefault(x => x.UnitCode == unitCode);
                            var productCategory = productCategories.SingleOrDefault(x => x.CategoryNumber == categoryNumber);

                            if (unit == null || productCategory == null)
                            {
                                throw new Exception("zła jednostka miary");
                            }

                            if (productCategory == null)
                            {
                                throw new Exception("zła kategoria produktu");
                            }



                            var productToImport = new ProductToImport
                            {
                                ProductNumber = productNumber,
                                OldProductNumber = oldProductNumber,
                                Name = productName,
                                UnitId = unit.Id,
                                ProductCategoryId = productCategory.Id,
                                Idx01 = idx01,
                                Idx02 = idx02,
                                ProductInDb = productInDb

                            };

                        productsToImport.Add(productToImport);
                        
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }

            }
        }

        return productsToImport;
    }

}
