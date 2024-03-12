using ClosedXML.Excel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesDifferencesToExcel;

public class ExportRoutesDifferencesToExcelQueryHandler : IRequestHandler<ExportRoutesDifferencesToExcelQuery,string>
{
    public async Task<string> Handle(ExportRoutesDifferencesToExcelQuery request, CancellationToken cancellationToken)
    {

        // PM> Install-Package ClosedXML
        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        string fileName = "ListOfProducts.xlsx";
        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string directory = @".\ExcelFiles";

        string fileNameWithFullPath = $@"{directory}\{fileName}";

        var routes = request.ComparedRoutes;

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Sample Sheet");

            int row = 1;
            worksheet.Cell(row, "A").Value = "CONTRACT";
            worksheet.Cell(row, "B").Value = "PART_NO";
            worksheet.Cell(row, "C").Value = "REVISION_NO";
            worksheet.Cell(row, "D").Value = "ALTERNATIVE_NO";
            
            worksheet.Cell(row, "E").Value = "OPERATION_NO";
            worksheet.Cell(row, "F").Value = "OPERATION_DESCRIPTION";

            worksheet.Cell(row, "G").Value = "WORK_CENTER_NO";
            worksheet.Cell(row, "H").Value = "LABOR_CLASS_NO";
            worksheet.Cell(row, "I").Value = "LABOR_RUN_FACTOR";
            worksheet.Cell(row, "J").Value = "MACH_RUN_FACTOR";
            worksheet.Cell(row, "K").Value = "CREW_SIZE";
            worksheet.Cell(row, "L").Value = "RUN_TIME_CODE";

            worksheet.Cell(row, "M").Value = "SETUP_LABOR_CLASS_NO";
            worksheet.Cell(row, "N").Value = "LABOR_SETUP_TIME";
            worksheet.Cell(row, "O").Value = "MACH_SETUP_TIME";
            worksheet.Cell(row, "P").Value = "SETUP_CREW_SIZE";

            worksheet.Cell(row, "Q").Value = "MOVE_TIME";
            worksheet.Cell(row, "R").Value = "OVERLAP";

            worksheet.Cell(row, "S").Value = "ALTERNATIVE_DESCRIPTION";

            worksheet.Cell(row, "T").Value = "TOOL_ID";
            worksheet.Cell(row, "U").Value = "TOOL_QUANTITY";
            worksheet.Cell(row, "W").Value = "IDX_LEVEL";
            worksheet.Cell(row, "V").Value = "IDX_CODE";


            worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Firebrick);
            worksheet.Row(row).Style.Font.SetBold();
            worksheet.Row(row).Style.Font.FontColor = XLColor.White;

            row++;
            foreach (var route in routes)
            {
                worksheet.Cell(row, "A").Value = "KT1";
                worksheet.Cell(row, "B").Value = route.PartNo;
                worksheet.Cell(row, "C").Value = route.RevisionNo;
                worksheet.Cell(row, "D").Value = route.AlternativeNo;
                
                worksheet.Cell(row, "E").Value = route.OperationNo;
                worksheet.Cell(row, "F").Value = route.MwtOperationDescription;

                worksheet.Cell(row, "G").Value = route.MwtWorkCenterNo;
                worksheet.Cell(row, "H").Value = route.MwtLaborClassNo;
                worksheet.Cell(row, "I").Value = route.MwtLaborRunFactor;
                worksheet.Cell(row, "J").Value = route.MwtMachRunFactor;
                worksheet.Cell(row, "K").Value = route.MwtCrewSize;
                worksheet.Cell(row, "L").Value = route.MwtRunTimeCode;

                worksheet.Cell(row, "M").Value = route.MwtSetupLaborClassNo;
                worksheet.Cell(row, "N").Value = route.MwtLaborSetupTime;
                worksheet.Cell(row, "O").Value = route.MwtMachSetupTime;
                worksheet.Cell(row, "P").Value = route.MwtSetupCrewSize;
                
                worksheet.Cell(row, "Q").Value = route.MwtMoveTime;
                worksheet.Cell(row, "R").Value = route.MwtOverlap;
                
                worksheet.Cell(row, "S").Value = route.MwtAlternativeDescription;
                
                worksheet.Cell(row, "T").Value = route.MwtToolId;
                worksheet.Cell(row, "U").Value = route.MwtToolQuantity;

                worksheet.Cell(row, "W").Value = route.IdxNo;
                worksheet.Cell(row, "V").Value = route.Idx;

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
