using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml;
using MediatR;
using Microsoft.Extensions.Configuration;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Common.Tools;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesToCsv;

public class ExportRoutesToCsvQueryHandler : IRequestHandler<ExportRoutesToCsvQuery>
{

    public ExportRoutesToCsvQueryHandler(IDateTimeService dateTimeService, IConfiguration configuration)
    {
        _dateTimeService = dateTimeService;
        _configuration = configuration;
    }

    private string _path = @".\ExcelFiles";
    private readonly IDateTimeService _dateTimeService;
    private readonly IConfiguration _configuration;

    public async Task Handle(ExportRoutesToCsvQuery request, CancellationToken cancellationToken)
    {

        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);
        
        string routesRevsHeadersFileName = "routes_01_revs_headers.csv";
        string routesAltsHeadersFileName = "routes_02_alts_headers.csv";
        string routesPositionsFileName = "routes_03_positions.csv";

        string routesRevsHeadersFile = $@"{_path}\{routesRevsHeadersFileName}";
        string routesAltsHeadersFile = $@"{_path}\{routesAltsHeadersFileName}";
        string routesPositionsFile = $@"{_path}\{routesPositionsFileName}";
        
        // Generujemy komplet plików csv
        await RoutesRevsCsv(request, routesRevsHeadersFile);
        await RoutesAltsCsv(request, routesAltsHeadersFile);
        await RoutesPositionsCsv(request, routesPositionsFile);

        // Kopiujemy te pliki na Oracle
        string destinationPath = _configuration.GetSection("PathToIfsCsv").Value;
        string user = _configuration.GetSection("IfsUser").Value;
        string pass = _configuration.GetSection("IfsPass").Value;
        string routesRevHeadersOraFile = System.IO.Path.Combine(destinationPath, routesRevsHeadersFileName);
        string routesAltHeadersOraFile = System.IO.Path.Combine(destinationPath, routesAltsHeadersFileName);
        string routesPositionsOraFile = System.IO.Path.Combine(destinationPath, routesPositionsFileName);

        NetworkShare.DisconnectFromShare(destinationPath, true); //Disconnect in case we are currently connected with our credentials;

        NetworkShare.ConnectToShare(destinationPath, user, pass); //Connect with the new credentials

        if (routesRevsHeadersFile != null)
        {
            File.Copy(routesRevsHeadersFile, routesRevHeadersOraFile, true);
            File.Copy(routesAltsHeadersFile, routesAltHeadersOraFile, true);
            File.Copy(routesPositionsFile, routesPositionsOraFile, true);
        }

        NetworkShare.DisconnectFromShare(destinationPath, false); //Disconnect from the server.

        return;
    }

    private async Task RoutesRevsCsv(ExportRoutesToCsvQuery request, string fileName)
    {
        var routes = request
                    .ComparedRoutes.GroupBy(x => new { x.PartNo, x.RevisionNo});
        
        await AddRevsHeading(fileName);

        foreach (var route in routes)
        {
            var dateTime = _dateTimeService.Now.ToString("dd.MM.yyyy");

            string line = $"KT1;{route.Key.PartNo};{route.Key.RevisionNo};{dateTime};";

            await WriteToFile(line, fileName, true);
        }
    }
    private async Task RoutesAltsCsv(ExportRoutesToCsvQuery request, string fileName)
    {
        var routes = request
                    .ComparedRoutes.GroupBy(x => new { x.PartNo, x.RevisionNo, x.AlternativeNo, x.MwtAlternativeDescription });

        await AddAltsHeading(fileName);

        foreach (var route in routes)
        {
            string line = $"KT1;{route.Key.PartNo};{route.Key.RevisionNo};{route.Key.AlternativeNo};{route.Key.MwtAlternativeDescription};";
            await WriteToFile(line, fileName, true);
        }
    }
    private async Task RoutesPositionsCsv(ExportRoutesToCsvQuery request, string fileName)
    {
        var routes = request
                    .ComparedRoutes;

        await AddPositionsHeading(fileName);

        foreach (var route in routes)
        {
            var ifsOperationId = 1;
            if (route.IfsOperationId.GetValueOrDefault() > 0)
            {
                ifsOperationId = route.IfsOperationId.GetValueOrDefault(); 
            }


            string line1a = $"KT1;{route.PartNo};{route.RevisionNo};{route.AlternativeNo};{route.OperationNo};{route.MwtOperationDescription};";
            string line1b = $"{route.MwtWorkCenterNo};{route.MwtLaborClassNo};{route.MwtLaborRunFactor};{route.MwtMachRunFactor};{route.MwtCrewSize};{route.MwtRunTimeCode};";
            string line1c = $"{route.MwtSetupLaborClassNo};{route.MwtLaborSetupTime};{route.MwtMachSetupTime};{route.MwtSetupCrewSize};";
            string line1d = $"{route.MwtMoveTime};{route.MwtOverlap};{ifsOperationId};";
            string line = line1a + line1b + line1c + line1d;

            await WriteToFile(line, fileName, true);
        }
    }

    private async Task AddAltsHeading(string fileNameWithFullPath)
    {

        var heading1 =
          "10;"
        + "20;"
        + "30;"
        + "40;"
        + "50;";
      

        await WriteToFile(heading1, fileNameWithFullPath, false);

        var heading2 =
                  "CONTRACT;"
                + "PART_NO;"
                + "REVISION_NO;"
                + "ALTERNATIVE_NO;"
                + "ALTERNATIVE_DESCRIPTION;";

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
        + "90;"
        + "100;"
        + "110;"
        + "120;"

        + "130;"
        + "140;"
        + "150;"
        + "160;"

        + "170;"
        + "180;"
        + "190;";

        await WriteToFile(heading1, fileNameWithFullPath, false);

        var heading2 =
                  "CONTRACT;"
                + "PART_NO;"
                + "REVISION_NO;"
                + "ALTERNATIVE_NO;"
                + "OPERATION_NO;"
                + "OPERATION_DESCRIPTION;"

                + "WORK_CENTER_NO;"
                + "LABOR_CLASS_NO;"
                + "LABOR_RUN_FACTOR;"
                + "MACH_RUN_FACTOR;"
                + "CREW_SIZE;"
                + "RUN_TIME_CODE;"

                + "SETUP_LABOR_CLASS_NO;"
                + "LABOR_SETUP_TIME;"
                + "MACH_SETUP_TIME;"
                + "SETUP_CREW_SIZE;"

                + "MOVE_TIME;"
                + "OVERLAP;"
                + "OPERATION_ID;";

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
