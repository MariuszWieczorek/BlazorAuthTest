using ClosedXML.Excel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.Products.OkcSpec.Queries.ExportOkcSpecToExcel;

public class ExportOkcSpecToExcelQueryHandler : IRequestHandler<ExportOkcSpecToExcelQuery,string>
{
    public async Task<string> Handle(ExportOkcSpecToExcelQuery request, CancellationToken cancellationToken)
    {

        // PM> Install-Package ClosedXML
        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        string fileName = "ListOfOkcSpec.xlsx";
        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string directory = @".\ExcelFiles";

        string fileNameWithFullPath = $@"{directory}\{fileName}";

        var OkcSpec = request.OkcSpec;

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Sample Sheet");

            int row = 1;
            worksheet.Cell(row, "A").Value = "Id";
            worksheet.Cell(row, "B").Value = "Indeks";
            worksheet.Cell(row, "C").Value = "Nazwa";
            worksheet.Cell(row, "D").Value = "KT";
            worksheet.Cell(row, "E").Value = "[gram/m2]";
            worksheet.Cell(row, "F").Value = "szerokosc [mm]";
            worksheet.Cell(row, "G").Value = "kąt cięcia";
            worksheet.Cell(row, "H").Value = "odl. między cięciami";
            worksheet.Cell(row, "I").Value = "Pole 1 szt. [m2]";
            worksheet.Cell(row, "J").Value = "Waga 1 szt. [kg]";
            worksheet.Cell(row, "K").Value = "przel. [kg/mb]";





            worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Firebrick);
            worksheet.Row(row).Style.Font.SetBold();
            worksheet.Row(row).Style.Font.FontColor = XLColor.White;

            row++;
            foreach (var product in OkcSpec)
            {
                worksheet.Cell(row, "A").Value = product.Id;
                worksheet.Cell(row, "B").Value = product.ProductNumber;
                worksheet.Cell(row, "C").Value = product.Name;
                worksheet.Cell(row, "D").Value = product.TechCardNumber;
                worksheet.Cell(row, "E").Value = product.OkgWagaGramNaM2;
                worksheet.Cell(row, "F").Value = product.OkcSzerokosc;
                worksheet.Cell(row, "G").Value = product.OkcKatCiecia;
                worksheet.Cell(row, "H").Value = product.DistanceBeetwenCuts;
                worksheet.Cell(row, "I").Value = product.CordArea;
                worksheet.Cell(row, "J").Value = product.CordWeight;
                worksheet.Cell(row, "K").Value = product.OkcPrzelMbNaKg;
                
                

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
