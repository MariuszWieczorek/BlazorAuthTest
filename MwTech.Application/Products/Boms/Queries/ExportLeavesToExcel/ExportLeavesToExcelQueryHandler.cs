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

namespace MwTech.Application.Products.Boms.Queries.ExportLeavesToExcel;

public class ExportLeavesToExcelQueryHandler : IRequestHandler<ExportLeavesToExcelQuery,string>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductCostService _productCostService;
    private readonly IProductService _productWeightService;

    public ExportLeavesToExcelQueryHandler(IApplicationDbContext context,
        IProductCostService productCostService,
        IProductService productWeightService
        )
    {
        _context = context;
        _productCostService = productCostService;
        _productWeightService = productWeightService;
    }
    public async Task<string> Handle(ExportLeavesToExcelQuery request, CancellationToken cancellationToken)
    {




        // PM> Install-Package ClosedXML
        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        string fileName = "ListOfProducts.xlsx";
        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string directory = @".\ExcelFiles";

        string fileNameWithFullPath = $@"{directory}\{fileName}";

        var bomLeaves = await GetBomLeaves(request);



        var totalQty = Math.Round(bomLeaves.Sum(x => x.FinalPartProductQty), 2);
        var totalQty2 = Math.Round(bomLeaves.Where(x => x.PartOnProductionOrder).Sum(x => x.FinalPartProductQty), 2);
        var totalQty3 = Math.Round(bomLeaves.Where(x => x.PartOnProductionOrder && x.PartMaterialPrice != 0).Sum(x => x.FinalPartProductQty), 2);

        var totalVolume2 = Math.Round(bomLeaves.Where(x => x.PartOnProductionOrder).Sum(x => x.PartVolume.GetValueOrDefault()), 2);

        var totalRubberQty = bomLeaves.Sum(x => x.PartRubberQty);
        var totalPhr = bomLeaves.Sum(x => x.PartPhr);

        decimal totalMaterialCost = Math.Round((decimal)bomLeaves.Sum(x => x.PartMaterialCost), 2);
        decimal totalMaterialCost2 = Math.Round((decimal)bomLeaves.Where(x => x.PartOnProductionOrder).Sum(x => x.PartMaterialCost), 2);



        decimal unitMaterialCost = 0M;
        decimal unitMaterialCost2 = 0M;
        decimal unitMaterialCost3 = 0M;

        if (totalQty != 0)
        {
            unitMaterialCost = Math.Round(totalMaterialCost / totalQty, 2);
        }

        if (totalQty2 != 0)
        {
            unitMaterialCost2 = Math.Round(totalMaterialCost2 / totalQty2, 2);
        }

        if (totalQty3 != 0)
        {
            unitMaterialCost3 = Math.Round(totalMaterialCost2 / totalQty3, 2);
        }

        decimal totalRubberContent = 0;
        decimal totalDensity = 0;

        if (totalQty2 != 0)
        {
            totalRubberContent = Math.Round(((decimal)totalRubberQty / (decimal)totalQty2) * 100, 2);
        }


        if (totalVolume2 != 0)
        {
            totalDensity = Math.Round(((decimal)totalQty2 / (decimal)totalVolume2), 2);
        }

        




        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Sample Sheet");

            int row = 1;


            worksheet.Cell(row, "A").Value = "Produkt";
            worksheet.Cell(row, "B").Value = "Komponent Indeks";
            worksheet.Cell(row, "C").Value = "Komponent Nazwa";
            worksheet.Cell(row, "D").Value = "Komponent Ilość";
            worksheet.Cell(row, "E").Value = "Zużywalny";
            worksheet.Cell(row, "F").Value = "Jm";
            worksheet.Cell(row, "G").Value = "zaw. kauczuku [%]";
            worksheet.Cell(row, "H").Value = "zaw. kauczuku [kg]";
            worksheet.Cell(row, "I").Value = "phr";
            worksheet.Cell(row, "J").Value = "gęstość [kg/m3]";
            worksheet.Cell(row, "K").Value = "objętość [m3]";
            worksheet.Cell(row, "L").Value = "udział komponentu [%]";
            worksheet.Cell(row, "M").Value = "id wagi";
            worksheet.Cell(row, "N").Value = "cena";
            worksheet.Cell(row, "O").Value = "wartość";


            worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Firebrick);
            worksheet.Row(row).Style.Font.SetBold();
            worksheet.Row(row).Style.Font.FontColor = XLColor.White;

            row++;
            foreach (var position in bomLeaves)
            {

                var percent = Math.Round(100 * position.FinalPartProductQty / totalQty2, 3);

                worksheet.Cell(row, "A").Value = position.SetProductNumber.Trim();
                worksheet.Cell(row, "B").Value = position.PartProductNumber.Trim();
                worksheet.Cell(row, "C").Value = position.PartProductName.Trim();
                worksheet.Cell(row, "D").Value = position.FinalPartProductQty;
                worksheet.Cell(row, "E").Value = position.PartOnProductionOrder == true ? "TAK" : "";
                worksheet.Cell(row, "F").Value = position.PartUnit;
                worksheet.Cell(row, "G").Value = position.PartContentsOfRubber != 0 ? position.PartContentsOfRubber : "";
                worksheet.Cell(row, "H").Value = position.PartRubberQty != 0 ? position.PartRubberQty : "";
                worksheet.Cell(row, "I").Value = position.PartPhr;
                worksheet.Cell(row, "J").Value = position.PartDensity;
                worksheet.Cell(row, "K").Value = position.PartVolume;
                worksheet.Cell(row, "L").Value = percent;
                worksheet.Cell(row, "M").Value = position.PartScalesId;
                worksheet.Cell(row, "N").Value = position.PartMaterialPrice;
                worksheet.Cell(row, "O").Value = position.PartMaterialCost;




               
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

    private async Task<List<BomTree>> GetBomLeaves(ExportLeavesToExcelQuery request)
    {

        int productVersionId = request.ProductVersionId;

        if (productVersionId == 0)
        {
            productVersionId = _context.ProductVersions
                .SingleOrDefault(x => x.ProductId == request.ProductId && x.DefaultVersion == true).Id;
        }


        List<BomTree> bomsTree = await _context.BomTrees
                     .FromSqlInterpolated($"select * from dbo.mwtech_bom_cte({request.ProductId},{productVersionId})")
                     .Where(x => x.HowManyParts == 0)
                     .OrderByDescending(x => x.Level)
                     .ThenBy(x => x.SetProductNumber)
                     .ThenBy(x => x.PartScalesId)
                     .ThenBy(x => x.PartOrdinalNo)
                     .ThenBy(x => x.PartProductNumber)
                     .AsNoTracking()
                     .ToListAsync();




        bomsTree = CalculatePhr(bomsTree);
        bomsTree = CalculateVolume(bomsTree);
        bomsTree = await GetPrices(bomsTree);

        return bomsTree;
    }



    private List<BomTree> CalculatePhr(List<BomTree> boms)
    {
        foreach (var item in boms)
        {
            item.PartRubberQty = Math.Round(Math.Round(item.FinalPartProductQty, 2) * (decimal)item.PartContentsOfRubber * 0.01m, 2);
        }
        var totalRubberQty = Math.Round((decimal)boms.Where(x => x.PartOnProductionOrder).Sum(x => x.PartRubberQty), 2);

        foreach (var item in boms)
        {
            if (totalRubberQty != 0 && item.PartOnProductionOrder)
            {
                item.PartPhr = Math.Round((item.FinalPartProductQty / (decimal)totalRubberQty) * 100, 2);
            }
        }

        return boms;
    }


    private List<BomTree> CalculateVolume(List<BomTree> boms)
    {
        foreach (var item in boms)
        {
            if ((decimal)item.PartDensity != 0 && item.PartOnProductionOrder)
            {
                item.PartVolume = Math.Round(Math.Round(item.FinalPartProductQty, 2) / (decimal)item.PartDensity, 2);
            }
            else
            {
                item.PartVolume = 0;
            }

        }
        var totalVolume = Math.Round((decimal)boms.Where(x => x.PartOnProductionOrder).Sum(x => x.PartVolume), 2);

        return boms;
    }


    private async Task<List<BomTree>> GetPrices(List<BomTree> boms)
    {
        foreach (var item in boms)
        {
            item.PartMaterialPrice = await _productCostService.GetProductPrice(item.PartProductId);
            item.PartMaterialCost = (decimal)Math.Round((decimal)item.PartMaterialPrice * item.FinalPartProductQty, 2);
            var x = item.PartMaterialCost;

        }


        return boms;
    }

}
