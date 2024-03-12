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

namespace MwTech.Application.Products.Boms.Queries.ExportBomsToExcel;

public class ExportBomsToExcelQueryHandler : IRequestHandler<ExportBomsToExcelQuery,string>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductCostService _productCostService;
    private readonly IProductService _productWeightService;

    public ExportBomsToExcelQueryHandler(IApplicationDbContext context,
        IProductCostService productCostService,
        IProductService productWeightService
        )
    {
        _context = context;
        _productCostService = productCostService;
        _productWeightService = productWeightService;
    }
    public async Task<string> Handle(ExportBomsToExcelQuery request, CancellationToken cancellationToken)
    {




        // PM> Install-Package ClosedXML
        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        string fileName = "ListOfProducts.xlsx";
        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string directory = @".\ExcelFiles";

        string fileNameWithFullPath = $@"{directory}\{fileName}";

        var boms = await GetBoms(request);


        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Sample Sheet");

            int row = 1;


            worksheet.Cell(row, "A").Value = "Produkt Indeks";
            worksheet.Cell(row, "B").Value = "Produkt Ilość";
            worksheet.Cell(row, "C").Value = "Wersja Nr";
            worksheet.Cell(row, "D").Value = "Wersja Nazwa";
            worksheet.Cell(row, "E").Value = "Lp";
            worksheet.Cell(row, "F").Value = "Kategoria";
            worksheet.Cell(row, "G").Value = "Komponent Indeks";
            worksheet.Cell(row, "H").Value = "Komponent Nazwa";
            worksheet.Cell(row, "I").Value = "Komponent Ilość";
            worksheet.Cell(row, "J").Value = "Komponent Nadmiar";
            worksheet.Cell(row, "K").Value = "Komponent Razem";
            worksheet.Cell(row, "L").Value = "Jm";
            worksheet.Cell(row, "M").Value = "Cena";
            worksheet.Cell(row, "N").Value = "Koszt";
            worksheet.Cell(row, "O").Value = "Nawrót";
            worksheet.Cell(row, "P").Value = "Opis";
            worksheet.Cell(row, "Q").Value = "Data Importu ceny";
            worksheet.Cell(row, "R").Value = "Data Wyliczenia Ceny";



            worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Firebrick);
            worksheet.Row(row).Style.Font.SetBold();
            worksheet.Row(row).Style.Font.FontColor = XLColor.White;

            row++;
            foreach (var position in boms)
            {
                var TotalQty = position.PartQty + (position.Excess * position.PartQty * 0.01M);

                worksheet.Cell(row, "A").Value = @position?.Set?.ProductNumber;
                worksheet.Cell(row, "B").Value = @position?.SetVersion?.ProductQty;
                worksheet.Cell(row, "C").Value = @position?.SetVersion?.VersionNumber;
                worksheet.Cell(row, "D").Value = @position?.SetVersion?.Name;
                worksheet.Cell(row, "E").Value = position?.OrdinalNumber;
                worksheet.Cell(row, "F").Value = position?.Part?.ProductCategory?.Name;
                worksheet.Cell(row, "G").Value = position?.Part?.ProductNumber;
                worksheet.Cell(row, "H").Value = position?.Part?.Name;
                worksheet.Cell(row, "I").Value = position?.PartQty;
                worksheet.Cell(row, "J").Value = position?.Excess;
                worksheet.Cell(row, "K").Value = TotalQty;
                worksheet.Cell(row, "L").Value = position?.Part?.Unit?.Name;
                worksheet.Cell(row, "M").Value = @position?.Cost;
                worksheet.Cell(row, "N").Value = @position?.TotalCost;
                worksheet.Cell(row, "O").Value = position?.Part?.ReturnedFromProd == true ? "TAK" : "";
                
                worksheet.Cell(row, "P").Value = @position?.CostDescription;
                worksheet.Cell(row, "Q").Value = @position?.ImportedDate;
                worksheet.Cell(row, "R").Value = @position?.CalculatedDate;


               
                //worksheet.Cell(row, 3).RichText.AddText(project.Title).SetFontColor(XLColor.Blue).SetBold();


                // Add the text parts
                /*
                var cell = worksheet.Cell(row, "D");
                cell.RichText
                  .AddText("Hello").SetFontColor(XLColor.Red)
                  .AddText(" BIG ").SetFontColor(XLColor.Blue).SetBold()
                  .AddText("World").SetFontColor(XLColor.Red);
                */

                if (row % 2 == 0)
                {
                    // worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Khaki);
                    // worksheet.Row(row).Style.Border.SetOutsideBorderColor(XLColor.Blue);
                    // worksheet.Row(row).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);

                }
                worksheet.Row(row).AdjustToContents();

                // .FromArgb(0xEEFFEE)

                row++;
            }

            for (int i = 1; i < 20; i++)
            {
                worksheet.Column(i).AdjustToContents();
                worksheet.Column(i).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;

            }



            worksheet.SheetView.FreezeRows(1);
            worksheet.AutoFilter.Clear();
            worksheet.RangeUsed().SetAutoFilter();


            // worksheet.Cell($"A{row}").FormulaA1 = "=MID(A1, 7, 5)";
            workbook.SaveAs(fileNameWithFullPath);
        }


        return fileNameWithFullPath;
    }

    private async Task<List<Bom>> GetBoms(ExportBomsToExcelQuery request)
    {
        var boms = await _context.Boms
                        .Include(x => x.Set)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.Set)
                        .ThenInclude(x => x.ProductCategory)
                        .Include(x => x.Part)
                        .ThenInclude(x => x.Unit)
                        .Include(x => x.Part)
                        .ThenInclude(x => x.ProductCategory)
                        .Include(x=>x.SetVersion)
                        .Where(x => x.SetVersionId == request.ProductVersionId && x.SetId == request.ProductId)
                        .OrderBy(x => x.OrdinalNumber)
                        .ThenBy(x => x.Part.ProductNumber)
                        .AsNoTracking()
                        .ToListAsync();

        boms = _productWeightService.CalculateWeight(boms);
        boms = await _productCostService.GetBomComponentsCosts(boms);

        return boms;
    }


}
