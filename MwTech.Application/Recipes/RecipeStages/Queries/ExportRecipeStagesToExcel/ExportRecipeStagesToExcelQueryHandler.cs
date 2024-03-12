using ClosedXML.Excel;
using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.Recipes.RecipeStages.Queries.ExportRecipeStagesToExcel;

public class ExportRecipeStagesToExcelQueryHandler : IRequestHandler<ExportRecipeStagesToExcelQuery, string>
{
    private readonly IRecipeService _recipeService;

    public ExportRecipeStagesToExcelQueryHandler(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    public async Task<string> Handle(ExportRecipeStagesToExcelQuery request, CancellationToken cancellationToken)
    {



        string fileName = "ListOfRecipeStages.xlsx";
        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string directory = @".\ExcelFiles";

        string fileNameWithFullPath = $@"{directory}\{fileName}";

        var RecipeStages = await _recipeService.GetRecipeVersionStages(request.RecipeVersionId);

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Sample Sheet");

            int row = 1;
            worksheet.Cell(row, "A").Value = "Nr Cyklu";
            worksheet.Cell(row, "B").Value = "Poj. Miksera";

            worksheet.Cell(row, "C").Value = "Indeks";
            worksheet.Cell(row, "D").Value = "Nazwa";
            worksheet.Cell(row, "E").Value = "PHR";
            worksheet.Cell(row, "F").Value = "gęstość [gram/cm3]";

            worksheet.Cell(row, "G").Value = "Ilość [kg]";
            worksheet.Cell(row, "H").Value = "Objętość [dm3]";

            worksheet.Cell(row, "I").Value = "Ilość 2 [kg]";
            worksheet.Cell(row, "J").Value = "Objętość 2 [dm3]";

            worksheet.Cell(row, "K").Value = "Factor";
            worksheet.Cell(row, "L").Value = "Skład";
            worksheet.Cell(row, "M").Value = "Procent";
            worksheet.Cell(row, "N").Value = "Cana_za_kg";
            worksheet.Cell(row, "O").Value = "Cana_x_ilość";


            worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Firebrick);
            worksheet.Row(row).Style.Font.SetBold();
            worksheet.Row(row).Style.Font.FontColor = XLColor.White;

            row++;



            foreach (var stage in RecipeStages)
            {

                foreach (var item in stage.RecipePositions)
                {
                    row++;

                    worksheet.Cell(row, "A").Value = stage.StageNo;
                    worksheet.Cell(row, "B").Value = stage.MixerVolume;

                    worksheet.Cell(row, "C").Value = item.Product?.ProductNumber;
                    worksheet.Cell(row, "D").Value = item.Product?.Name;
                    worksheet.Cell(row, "E").Value = item.PartPhr;
                    worksheet.Cell(row, "F").Value = item.Product.Density;

                    worksheet.Cell(row, "G").Value = item.ProductQty;
                    worksheet.Cell(row, "H").Value = item.PartVolume;

                    worksheet.Cell(row, "I").Value = item.PartQty2;
                    worksheet.Cell(row, "J").Value = item.PartVolume2;
                    worksheet.Cell(row, "K").Value = stage.Factor;
                    worksheet.Cell(row, "L").Value = item.LastStageWeight;
                    worksheet.Cell(row, "M").Value = item.LastStagePercent;
                    worksheet.Cell(row, "N").Value = item.MaterialUnitCost;
                    worksheet.Cell(row, "O").Value = item.MaterialTotalCost;


                    worksheet.Cell(row, "P").Value = item.PartRubberContent;
                    worksheet.Cell(row, "R").Value = item.PartRubberContent2;

                    if (row % 2 == 0)
                    {
                        // worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Khaki);
                        // worksheet.Row(row).Style.Border.SetOutsideBorderColor(XLColor.Blue);
                        // worksheet.Row(row).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);

                    }
                    worksheet.Row(row).AdjustToContents();

                    // .FromArgb(0xEEFFEE)




                }

                row++;
                worksheet.Cell(row, "E").Value = stage.TotalPhr;
                worksheet.Cell(row, "E").Style.Fill.BackgroundColor = XLColor.LightCoral;


                worksheet.Cell(row, "G").Value = stage.TotalQty;
                worksheet.Cell(row, "H").Value = stage.TotalVolume;
                worksheet.Cell(row, "I").Value = stage.TotalQty2;
                worksheet.Cell(row, "J").Value = stage.TotalVolume2;


                worksheet.Cell(row, "P").Value = stage.TotalRubberContent;
                worksheet.Cell(row, "R").Value = stage.TotalRubberContent2;
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
