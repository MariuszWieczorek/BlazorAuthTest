using ClosedXML.Excel;
using MediatR;

namespace MwTech.Application.Recipes.Recipes.Queries.ExportRecipesToExcel;

public class ExportRecipesToExcelQueryHandler : IRequestHandler<ExportRecipesToExcelQuery,string>
{
    public async Task<string> Handle(ExportRecipesToExcelQuery request, CancellationToken cancellationToken)
    {

        // PM> Install-Package ClosedXML
        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        string fileName = "ListOfRecipes.xlsx";
        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string directory = @".\ExcelFiles";

        string fileNameWithFullPath = $@"{directory}\{fileName}";

        var Recipes = request.Recipes;

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Sample Sheet");

            int row = 1;
            worksheet.Cell(row, "A").Value = "Id";
            worksheet.Cell(row, "B").Value = "Symbol";
            worksheet.Cell(row, "C").Value = "Nazwa Mieszanki";
            worksheet.Cell(row, "D").Value = "Nazwa Wersji";

            worksheet.Cell(row, "E").Value = "Produkt Finalny";
            worksheet.Cell(row, "F").Value = "Nawrót z Produkcji";

            worksheet.Cell(row, "G").Value = "Opis";
            worksheet.Cell(row, "H").Value = "Kategoria";
            

            worksheet.Cell(row, "I").Value = "Koszt";
            worksheet.Cell(row, "J").Value = "Materiały";
            worksheet.Cell(row, "K").Value = "Robocizna";
            worksheet.Cell(row, "L").Value = "Narzuty";
            worksheet.Cell(row, "M").Value = "MatBezNawrotow";


            worksheet.Row(row).Style.Fill.SetBackgroundColor(XLColor.Firebrick);
            worksheet.Row(row).Style.Font.SetBold();
            worksheet.Row(row).Style.Font.FontColor = XLColor.White;

            row++;
            foreach (var Recipe in Recipes)
            {
                worksheet.Cell(row, "A").Value = Recipe.Id;
                worksheet.Cell(row, "B").GetRichText().AddText(Recipe.RecipeNumber).SetBold();
                worksheet.Cell(row, "C").Value = Recipe.Name;
                worksheet.Cell(row, "D").Value = Recipe.VersionName;

                worksheet.Cell(row, "E").Value = Recipe.ProductNumber;
                worksheet.Cell(row, "F").Value = Recipe.ScrapNumber;

                worksheet.Cell(row, "G").Value = Recipe.Description?.Trim();
                worksheet.Cell(row, "H").Value = Recipe.RecipeCategory?.Name;
                
                
                worksheet.Cell(row, "I").Value = Recipe.TotalCost;
                worksheet.Cell(row, "J").Value = Recipe.MaterialCost;
                worksheet.Cell(row, "K").Value = Recipe.LabourCost;
                worksheet.Cell(row, "L").Value = Recipe.MarkupCost;
                worksheet.Cell(row, "M").Value = Recipe.MaterialUnitCostWithoutProcessReturn;
                              

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
