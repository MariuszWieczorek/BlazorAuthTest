using MediatR;
using Microsoft.Extensions.Configuration;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Common.Tools;

namespace MwTech.Application.IfsSourceRecipes.Queries.ExportRecipesToCsv;

public class ExportRecipesToCsvQueryHandler : IRequestHandler<ExportRecipesToCsvQuery>
{

    public ExportRecipesToCsvQueryHandler(IDateTimeService dateTimeService, IConfiguration configuration)
    {
        _dateTimeService = dateTimeService;
        _configuration = configuration;
    }

    private string _path = @".\ExcelFiles";
    private readonly IDateTimeService _dateTimeService;
    private readonly IConfiguration _configuration;

    public async Task Handle(ExportRecipesToCsvQuery request, CancellationToken cancellationToken)
    {

        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string recipesRevsHeadersFileName = "recipes_01_revs_headers.csv";
        string recipesAltsHeadersFileName = "recipes_02_alts_headers.csv";
        string recipesPositionsFileName = "recipes_03_positions.csv";

        string recipesRevsHeadersFile = $@"{_path}\{recipesRevsHeadersFileName}";
        string recipesAltsHeadersFile = $@"{_path}\{recipesAltsHeadersFileName}";
        string recipesPositionsFile = $@"{_path}\{recipesPositionsFileName}";

        // Generujemy komplet plików csv
        await RecipesRevsCsv(request, recipesRevsHeadersFile);
        await RecipesAltsCsv(request, recipesAltsHeadersFile);
        await RecipesPositionsCsv(request, recipesPositionsFile);

        // Kopiujemy te pliki na Oracle
        string destinationPath = _configuration.GetSection("PathToIfsCsv").Value;
        string user = _configuration.GetSection("IfsUser").Value;
        string pass = _configuration.GetSection("IfsPass").Value;
        string recipesRevHeadersOraFile = System.IO.Path.Combine(destinationPath, recipesRevsHeadersFileName);
        string recipesAltHeadersOraFile = System.IO.Path.Combine(destinationPath, recipesAltsHeadersFileName);
        string recipesPositionsOraFile = System.IO.Path.Combine(destinationPath, recipesPositionsFileName);

        NetworkShare.DisconnectFromShare(destinationPath, true); //Disconnect in case we are currently connected with our credentials;

        NetworkShare.ConnectToShare(destinationPath, user, pass); //Connect with the new credentials

        if (recipesRevsHeadersFile != null)
        {
            File.Copy(recipesRevsHeadersFile, recipesRevHeadersOraFile, true);
            File.Copy(recipesAltsHeadersFile, recipesAltHeadersOraFile, true);
            File.Copy(recipesPositionsFile, recipesPositionsOraFile, true);
        }

        NetworkShare.DisconnectFromShare(destinationPath, false); //Disconnect from the server.

        return;
    }

    private async Task RecipesRevsCsv(ExportRecipesToCsvQuery request, string fileName)
    {
        var Recipes = request
                    .ComparedRecipes.GroupBy(x => new { x.PartNo, x.RevisionNo });

        await AddRevsHeading(fileName);

        foreach (var Recipe in Recipes)
        {
            var dateTime = _dateTimeService.Now.ToString("dd.MM.yyyy");

            string line = $"KT1;{Recipe.Key.PartNo};{Recipe.Key.RevisionNo};{dateTime};";

            await WriteToFile(line, fileName, true);
        }
    }
    private async Task RecipesAltsCsv(ExportRecipesToCsvQuery request, string fileName)
    {
        var recipes = request
                    .ComparedRecipes.GroupBy(x => new { x.PartNo, x.RevisionNo, x.AlternativeNo });

        await AddAltsHeading(fileName);

        foreach (var recipe in recipes)
        {
            string line = $"KT1;{recipe.Key.PartNo};{recipe.Key.RevisionNo};{recipe.Key.AlternativeNo};";
            await WriteToFile(line, fileName, true);
        }
    }
    private async Task RecipesPositionsCsv(ExportRecipesToCsvQuery request, string fileName)
    {
        var recipes = request
                    .ComparedRecipes;

        await AddPositionsHeading(fileName);

        foreach (var recipe in recipes)
        {
            string line1a = $"KT1;{recipe.PartNo};{recipe.RevisionNo};{recipe.AlternativeNo};{recipe.LineSequence};";
            string line1b = $"{recipe.MwComponentPart};{recipe.MwQtyPerAssembly};{recipe.MwShrinkageFactor};{recipe.MwConsumptionItem};";

            string line = line1a + line1b;

            await WriteToFile(line, fileName, true);
        }
    }

    private async Task AddAltsHeading(string fileNameWithFullPath)
    {

        var heading1 =
          "10;"
        + "20;"
        + "30;"
        + "40;";


        await WriteToFile(heading1, fileNameWithFullPath, false);

        var heading2 =
                  "CONTRACT;"
                + "PART_NO;"
                + "REVISION_NO;"
                + "ALTERNATIVE_NO;";

        await WriteToFile(heading2, fileNameWithFullPath, true);
    }
    private async Task AddRevsHeading(string fileNameWithFullPath)
    {

        var heading1 =
          "10;"
        + "20;"
        + "30;"
        + "40;";


        await WriteToFile(heading1, fileNameWithFullPath, false);

        var heading2 =
                  "CONTRACT;"
                + "PART_NO;"
                + "REVISION_NO;"
                + "PHASE_IN_DATE;";

        await WriteToFile(heading2, fileNameWithFullPath, true);
    }
    private async Task AddPositionsHeading(string fileNameWithFullPath)
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

        var heading2 =
                  "CONTRACT;"
                + "PART_NO;"
                + "REVISION_NO;"
                + "ALTERNATIVE_NO;"
                + "LINE_SEQUENCE;"

                + "COMPONENT_PART;"
                + "QTY_PER_ASSEMBLY;"
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
