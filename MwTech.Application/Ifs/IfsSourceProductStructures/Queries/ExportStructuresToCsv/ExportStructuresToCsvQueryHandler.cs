using MediatR;
using Microsoft.Extensions.Configuration;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Common.Tools;

namespace MwTech.Application.IfsSourceStructures.Queries.ExportStructuresToCsv;

public class ExportStructuresToCsvQueryHandler : IRequestHandler<ExportStructuresToCsvQuery>
{

    public ExportStructuresToCsvQueryHandler(IDateTimeService dateTimeService, IConfiguration configuration)
    {
        _dateTimeService = dateTimeService;
        _configuration = configuration;
    }

    private string _path = @".\ExcelFiles";
    private readonly IDateTimeService _dateTimeService;
    private readonly IConfiguration _configuration;

    public async Task Handle(ExportStructuresToCsvQuery request, CancellationToken cancellationToken)
    {

        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string StructuresRevsHeadersFileName = "Structures_01_revs_headers.csv";
        string StructuresAltsHeadersFileName = "Structures_02_alts_headers.csv";
        string StructuresPositionsFileName = "Structures_03_positions.csv";

        string StructuresRevsHeadersFile = $@"{_path}\{StructuresRevsHeadersFileName}";
        string StructuresAltsHeadersFile = $@"{_path}\{StructuresAltsHeadersFileName}";
        string StructuresPositionsFile = $@"{_path}\{StructuresPositionsFileName}";

        // Generujemy komplet plików csv
        await StructuresRevsCsv(request, StructuresRevsHeadersFile);
        await StructuresAltsCsv(request, StructuresAltsHeadersFile);
        await StructuresPositionsCsv(request, StructuresPositionsFile);

        // Kopiujemy te pliki na Oracle
        string destinationPath = _configuration.GetSection("PathToIfsCsv").Value;
        string user = _configuration.GetSection("IfsUser").Value;
        string pass = _configuration.GetSection("IfsPass").Value;
        string StructuresRevHeadersOraFile = System.IO.Path.Combine(destinationPath, StructuresRevsHeadersFileName);
        string StructuresAltHeadersOraFile = System.IO.Path.Combine(destinationPath, StructuresAltsHeadersFileName);
        string StructuresPositionsOraFile = System.IO.Path.Combine(destinationPath, StructuresPositionsFileName);

        NetworkShare.DisconnectFromShare(destinationPath, true); //Disconnect in case we are currently connected with our credentials;

        NetworkShare.ConnectToShare(destinationPath, user, pass); //Connect with the new credentials

        if (StructuresRevsHeadersFile != null)
        {
            File.Copy(StructuresRevsHeadersFile, StructuresRevHeadersOraFile, true);
            File.Copy(StructuresAltsHeadersFile, StructuresAltHeadersOraFile, true);
            File.Copy(StructuresPositionsFile, StructuresPositionsOraFile, true);
        }

        NetworkShare.DisconnectFromShare(destinationPath, false); //Disconnect from the server.

        return;
    }

    private async Task StructuresRevsCsv(ExportStructuresToCsvQuery request, string fileName)
    {
        var Structures = request
                    .ComparedStructures.GroupBy(x => new { x.PartNo, x.RevisionNo });

        await AddRevsHeading(fileName);

        foreach (var Structure in Structures)
        {
            var dateTime = _dateTimeService.Now.ToString("dd.MM.yyyy");

            string line = $"KT1;{Structure.Key.PartNo};{Structure.Key.RevisionNo};{dateTime};";

            await WriteToFile(line, fileName, true);
        }
    }
    private async Task StructuresAltsCsv(ExportStructuresToCsvQuery request, string fileName)
    {
        var Structures = request
                    .ComparedStructures.GroupBy(x => new { x.PartNo, x.RevisionNo, x.AlternativeNo });

        await AddAltsHeading(fileName);

        foreach (var Structure in Structures)
        {
            string line = $"KT1;{Structure.Key.PartNo};{Structure.Key.RevisionNo};{Structure.Key.AlternativeNo};";
            await WriteToFile(line, fileName, true);
        }
    }
    private async Task StructuresPositionsCsv(ExportStructuresToCsvQuery request, string fileName)
    {
        var Structures = request
                    .ComparedStructures;

        await AddPositionsHeading(fileName);

        foreach (var structure in Structures)
        {
            string line1a = $"KT1;{structure.PartNo};{structure.RevisionNo};{structure.AlternativeNo};{structure.LineSequence};";
            string line1b = $"{structure.MwComponentPart};{structure.MwQtyPerAssembly};{structure.MwShrinkageFactor};{structure.MwConsumptionItem};";

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
