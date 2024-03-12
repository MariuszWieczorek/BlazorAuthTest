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

namespace MwTech.Application.Products.ProductCosts.Commands.ImportProductsCostFromExcel;

public class ImportProductsCostFromExcelCommandHandler : IRequestHandler<ImportProductsCostFromExcelCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IComarchService _comarchService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IProductCostService _productCostService;

    public ImportProductsCostFromExcelCommandHandler(IApplicationDbContext context,
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
    public async Task Handle(ImportProductsCostFromExcelCommand request, CancellationToken cancellationToken)
    {

        string fileName = request.FileName;


        var productsCostToImport = await GetProductsCostFromExcel(fileName);
        var currentUserId = _currentUserService.UserId;

        if (productsCostToImport.Count() == 0)
            throw new Exception("Brak kosztów do zaimportowania");

        var period = new AccountingPeriod();

        try
        {
            period = _context.AccountingPeriods
            .SingleOrDefault(x => x.IsDefault);
        }
        catch (Exception)
        {

            throw new Exception("Więcej niż jeden domyślny okres, lub brak domyślnych okresów");
        }
        
        

        foreach (var item in productsCostToImport)
        {
            
         
                var productCost = _context.ProductCosts
                            .SingleOrDefault(x => x.ProductId == item.ProductId
                            && x.AccountingPeriodId == period.Id);

                if (productCost == null)
                {
                    var newProductCost = new ProductCost()
                    {
                        ProductId = item.ProductId,
                        AccountingPeriodId = period.Id,
                        CurrencyId = item.CurrencyId,
                        Cost = item.Price,
                        IsImported = true,
                        ImportedDate = DateTime.Now,
                        Description = item.Comment,
                        CreatedByUserId = currentUserId,
                        CreatedDate = _dateTimeService.Now

                    };

                    _context.ProductCosts.Add(newProductCost);
                }
                else
                {

                    productCost.CurrencyId = item.CurrencyId;
                    productCost.Cost = item.Price;
                    productCost.IsImported = true;
                    productCost.ImportedDate = DateTime.Now;
                    productCost.Description = item.Comment;
                    productCost.ModifiedByUserId = currentUserId;
                    productCost.ModifiedDate = _dateTimeService.Now;

                    _context.ProductCosts.Update(productCost);

                }

                await _context.SaveChangesAsync();
            


        }
        return;
    }


    private async Task<IEnumerable<ProductCostToImport>> GetProductsCostFromExcel(string fileName)
    {
        int licznik = 0;
        var productsCostToImport = new List<ProductCostToImport>();

        using (var excelWorkbook = new XLWorkbook(fileName))
        {
            var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
            foreach (var dataRow in nonEmptyDataRows)
            {
                if (dataRow.RowNumber() >= 2)
                {
                    string productNumber = dataRow.Cell(1).Value.ToString().Trim();
                    string productName = dataRow.Cell(2).Value.ToString().Trim();
                    string sPriceInEur = dataRow.Cell(3).Value.ToString().Trim();
                    string sPriceInUsd = dataRow.Cell(4).Value.ToString().Trim();
                    string sPriceInPln = dataRow.Cell(5).Value.ToString().Trim();


                    if (string.IsNullOrWhiteSpace(productNumber))
                        break;

                    decimal priceInEur = 0m;
                    decimal priceInUsd = 0m;
                    decimal priceInPln = 0m;

                    if (!String.IsNullOrEmpty(sPriceInEur))
                        priceInEur = Convert.ToDecimal(sPriceInEur);

                    if (!String.IsNullOrEmpty(sPriceInUsd))
                        priceInUsd = Convert.ToDecimal(sPriceInUsd);

                    if (!String.IsNullOrEmpty(sPriceInPln))
                        priceInPln = Convert.ToDecimal(sPriceInPln);

                    //Convert.ToDecimal(priceInEur);

                    decimal price = 0m;
                    Currency? currency = null;

                    if (priceInEur != 0)
                    {
                        price = priceInEur;
                        currency = _context.Currencies.SingleOrDefault(x => x.CurrencyCode == "EUR");
                    }

                    if (priceInUsd != 0)
                    {
                        price = priceInUsd;
                        currency = _context.Currencies.SingleOrDefault(x => x.CurrencyCode == "USD");
                    }

                    if (priceInPln != 0)
                    {
                        price = priceInPln;
                        currency = _context.Currencies.SingleOrDefault(x => x.CurrencyCode == "PLN");
                    }


                    var product = _context.Products.SingleOrDefault(x => x.ProductNumber == productNumber);
                    
                    if (product == null)
                    {
                        product = _context.Products.SingleOrDefault(x => x.OldProductNumber == productNumber);
                    }


                    if (product != null && price > 0)
                    {
                        var productCostToImport = new ProductCostToImport
                        {
                            ProductId = product.Id,
                            Price = price,
                            CurrencyId = currency.Id,
                            Comment = fileName
                        };

                        productsCostToImport.Add(productCostToImport);
                    }
                }
                
            }
        }

        return productsCostToImport;
    }

}
