using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml;
using MediatR;
using MwTech.Application.Common.Interfaces;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesRevHeadersToCsv;

public class ExportRoutesRevHeadersToCsvQueryHandler : IRequestHandler<ExportRoutesRevHeadersToCsvQuery, string>
{

    public ExportRoutesRevHeadersToCsvQueryHandler(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }

    private string _path = @".\ExcelFiles";
    private readonly IDateTimeService _dateTimeService;

    public async Task<string> Handle(ExportRoutesRevHeadersToCsvQuery request, CancellationToken cancellationToken)
    {

        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string fileName = "routes_headers_to_import.csv";
        string fileNameWithFullPath = $@"{_path}\{fileName}";

        var routes = request
            .ComparedRoutes.GroupBy(x=> new {x.PartNo, x.RevisionNo});
        await AddHeading(fileNameWithFullPath);

        foreach (var route in routes)
        {
            var dateTime = _dateTimeService.Now.ToString("dd.MM.yyyy");

            string line = $"KT1;{route.Key.PartNo};{route.Key.RevisionNo};{dateTime};";


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
