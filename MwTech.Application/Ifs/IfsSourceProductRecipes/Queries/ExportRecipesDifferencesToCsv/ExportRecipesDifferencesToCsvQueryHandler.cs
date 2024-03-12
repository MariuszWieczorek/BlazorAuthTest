using MediatR;

namespace MwTech.Application.IfsSourceRecipes.Queries.ExportRecipesDifferencesToCsv;

public class ExportRecipesDifferencesToCsvQueryHandler : IRequestHandler<ExportRecipesDifferencesToCsvQuery, string>
{

    private string _path = @".\ExcelFiles";

    public async Task<string> Handle(ExportRecipesDifferencesToCsvQuery request, CancellationToken cancellationToken)
    {

        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string fileName = "recipes_to_import.csv";
        string fileNameWithFullPath = $@"{_path}\{fileName}";

        var Recipes = request.ComparedRecipes;
        await AddHeading(fileNameWithFullPath);

        foreach (var Recipe in Recipes)
        {
            string line1a = $"KT1;{Recipe.PartNo};{Recipe.RevisionNo};{Recipe.AlternativeNo};{Recipe.LineSequence};";
            string line1b = $"{Recipe.MwComponentPart};{Recipe.MwQtyPerAssembly};{Recipe.MwShrinkageFactor};{Recipe.MwConsumptionItem};";

            string line = line1a + line1b;

            await WriteToFile(line, fileNameWithFullPath, true);


        }

        return fileNameWithFullPath;
    }

    private async Task AddHeading(string fileNameWithFullPath)
    {

        var heading1 =
          "10;"
        + "20;"
        + "30;"
        + "40;"
        + "50;"

        + "60;"
        + "70;"
        + "80;"
        + "90;";

        await WriteToFile(heading1, fileNameWithFullPath, false);

        // UWAGA: w IFS_PARTS_BY_WEIGHT wrzucam MwQtyPerAssembly

        var heading2 =
                  "CONTRACT;"
                + "PART_NO;"
                + "REVISION_NO;"
                + "ALTERNATIVE_NO;"
                + "LINE_SEQUENCE;"

                + "COMPONENT_PART;"
                + "IFS_PARTS_BY_WEIGHT;"
                + "SHRINKAGE_FACTOR;"
                + "CONSUMPTION_ITEM;";

        await WriteToFile(heading2, fileNameWithFullPath, true);
    }

    private async Task WriteToFile(string line, string fileNameWithFullPath, bool append)
    {

        using (StreamWriter sw = new StreamWriter(fileNameWithFullPath, append))
        {
            await sw.WriteLineAsync(line);
        }
    }

}
