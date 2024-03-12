using ClosedXML.Excel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.Products.Queries.ExportProductsToExcel;

public class ExportProductsToExcelQueryHandler : IRequestHandler<ExportProductsToExcelQuery,string>
{
    public async Task<string> Handle(ExportProductsToExcelQuery request, CancellationToken cancellationToken)
    {

        // PM> Install-Package ClosedXML
        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        string fileName = "ListOfProducts.xlsx";
        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string directory = @".\ExcelFiles";

        string fileNameWithFullPath = $@"{directory}\{fileName}";

        var products = request.Products;

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Sample Sheet");

            int row = 1;
            worksheet.Cell(row, "A").Value = "Id";
            worksheet.Cell(row, "B").Value = "Nazwa";
            worksheet.Cell(row, "C").Value = "Indeks";
            worksheet.Cell(row, "D").Value = "Stary Index";
            worksheet.Cell(row, "E").Value = "KT";
            worksheet.Cell(row, "F").Value = "Jm";
            worksheet.Cell(row, "G").Value = "Opis";
            worksheet.Cell(row, "H").Value = "Kategoria";
            worksheet.Cell(row, "I").Value = "Wersja";
            worksheet.Cell(row, "J").Value = "Materiały";
            worksheet.Cell(row, "K").Value = "Robocizna";
            worksheet.Cell(row, "L").Value = "Koszt";
            worksheet.Cell(row, "M").Value = "Ile komponentów";
            worksheet.Cell(row, "N").Value = "W ilu zestawach";
            worksheet.Cell(row, "O").Value = "Waga";
            worksheet.Cell(row, "P").Value = "Idx01";
            worksheet.Cell(row, "Q").Value = "Idx02";
            worksheet.Cell(row, "R").Value = "Gestosc";
            worksheet.Cell(row, "S").Value = "Kauczuk";
            worksheet.Cell(row, "T").Value = "Info";
            




            worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Firebrick);
            worksheet.Row(row).Style.Font.SetBold();
            worksheet.Row(row).Style.Font.FontColor = XLColor.White;

            row++;
            foreach (var product in products)
            {
                worksheet.Cell(row, "A").Value = product.Id;
                worksheet.Cell(row, "B").Value = product.Name;
                worksheet.Cell(row, "C").GetRichText().AddText(product.ProductNumber).SetBold();
                worksheet.Cell(row, "D").Value = product.OldProductNumber;
                worksheet.Cell(row, "E").Value = product.TechCardNumber;
                worksheet.Cell(row, "F").Value = product.Unit?.Name?.Trim();
                worksheet.Cell(row, "G").Value = product.Description?.Trim();
                worksheet.Cell(row, "H").Value = product.ProductCategory?.Name;
                worksheet.Cell(row, "I").Value = product.ProductVersions.FirstOrDefault(x => x.DefaultVersion)?.VersionNumber;
                worksheet.Cell(row, "J").Value = product.MaterialCost;
                worksheet.Cell(row, "K").Value = product.LabourCost;
                worksheet.Cell(row, "L").Value = product.Cost;
                worksheet.Cell(row, "M").Value = product.SetsCounter;
                worksheet.Cell(row, "N").Value = product.PartsCounter;
                worksheet.Cell(row, "O").Value = product.ProductWeight;
                worksheet.Cell(row, "P").Value = product.Idx01;
                worksheet.Cell(row, "Q").Value = product.Idx02;
                worksheet.Cell(row, "R").Value = product.Density;
                worksheet.Cell(row, "S").Value = product.ContentsOfRubber;
                worksheet.Cell(row, "T").Value = product.info;
                
                

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
                    worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Khaki);
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
}
