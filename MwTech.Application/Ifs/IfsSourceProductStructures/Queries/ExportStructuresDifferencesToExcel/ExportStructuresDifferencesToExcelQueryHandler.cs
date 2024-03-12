using ClosedXML.Excel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.IfsSourceStructures.Queries.ExportStructuresDifferencesToExcel;

public class ExportStructuresDifferencesToExcelQueryHandler : IRequestHandler<ExportStructuresDifferencesToExcelQuery,string>
{
    public async Task<string> Handle(ExportStructuresDifferencesToExcelQuery request, CancellationToken cancellationToken)
    {

        // PM> Install-Package ClosedXML
        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        string fileName = "ListOfProducts.xlsx";
        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string directory = @".\ExcelFiles";

        string fileNameWithFullPath = $@"{directory}\{fileName}";

        var Structures = request.ComparedStructures;

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Sample Sheet");

            int row = 1;
            worksheet.Cell(row, "A").Value = "CONTRACT";
            worksheet.Cell(row, "B").Value = "PART_NO";
            worksheet.Cell(row, "C").Value = "REVISION_NO";
            worksheet.Cell(row, "D").Value = "ALTERNATIVE_NO";
            worksheet.Cell(row, "E").Value = "LINE_SEQUENCE";

            worksheet.Cell(row, "F").Value = "COMPONENT_PART";
            worksheet.Cell(row, "G").Value = "QTY_PER_ASSEMBLY";
            worksheet.Cell(row, "H").Value = "SHRINKAGE_FACTOR";
            worksheet.Cell(row, "I").Value = "CONSUMPTION_ITEM";


            worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Firebrick);
            worksheet.Row(row).Style.Font.SetBold();
            worksheet.Row(row).Style.Font.FontColor = XLColor.White;

            row++;
            foreach (var Structure in Structures)
            {
                worksheet.Cell(row, "A").Value = "KT1";
                worksheet.Cell(row, "B").Value = Structure.PartNo;
                worksheet.Cell(row, "C").Value = Structure.RevisionNo;
                worksheet.Cell(row, "D").Value = Structure.AlternativeNo;
                worksheet.Cell(row, "E").Value = Structure.LineSequence;

                worksheet.Cell(row, "F").Value = Structure.MwComponentPart;
                worksheet.Cell(row, "G").Value = Structure.MwQtyPerAssembly;
                worksheet.Cell(row, "H").Value = Structure.MwShrinkageFactor;
                worksheet.Cell(row, "I").Value = Structure.MwConsumptionItem;


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
