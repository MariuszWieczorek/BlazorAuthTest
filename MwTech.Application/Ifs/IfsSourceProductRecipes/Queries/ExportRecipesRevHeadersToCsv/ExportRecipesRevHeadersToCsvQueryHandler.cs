using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.IfsSourceRecipes.Queries.ExportRecipesRevHeadersToCsv;

public class ExportRecipesRevHeadersToCsvQueryHandler : IRequestHandler<ExportRecipesRevHeadersToCsvQuery, string>
{

    public ExportRecipesRevHeadersToCsvQueryHandler(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }

    private string _path = @".\ExcelFiles";
    private readonly IDateTimeService _dateTimeService;

    public async Task<string> Handle(ExportRecipesRevHeadersToCsvQuery request, CancellationToken cancellationToken)
    {

        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string fileName = "recipes_headers_to_import.csv";
        string fileNameWithFullPath = $@"{_path}\{fileName}";

        var recipes = request
            .ComparedRecipes.GroupBy(x=> new {x.PartNo, x.RevisionNo});
        await AddHeading(fileNameWithFullPath);

        foreach (var recipe in recipes)
        {
            var dateTime = _dateTimeService.Now.ToString("dd.MM.yyyy");

            string line = $"KT1;{recipe.Key.PartNo};{recipe.Key.RevisionNo};{dateTime};";


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
        + "40;";
      

        await WriteToFile(heading1, fileNameWithFullPath, false);

        var heading2 =
                  "CONTRACT;"
                + "PART_NO;"
                + "REVISION_NO;"
                + "PHASE_IN_DATE;";

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
