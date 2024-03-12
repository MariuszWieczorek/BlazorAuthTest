using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml;
using MediatR;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesDifferencesToCsv;

public class ExportRoutesDifferencesToCsvQueryHandler : IRequestHandler<ExportRoutesDifferencesToCsvQuery, string>
{

    private string _path = @".\ExcelFiles";

    public async Task<string> Handle(ExportRoutesDifferencesToCsvQuery request, CancellationToken cancellationToken)
    {

        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string fileName = "routes_to_import.csv";
        string fileNameWithFullPath = $@"{_path}\{fileName}";

        var routes = request.ComparedRoutes;
        await AddHeading(fileNameWithFullPath);

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
