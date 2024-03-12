using MediatR;

namespace MwTech.Application.IfsSourceStructures.Queries.ExportStructuresDifferencesToCsv;

public class ExportStructuresDifferencesToCsvQueryHandler : IRequestHandler<ExportStructuresDifferencesToCsvQuery, string>
{

    private string _path = @".\ExcelFiles";

    public async Task<string> Handle(ExportStructuresDifferencesToCsvQuery request, CancellationToken cancellationToken)
    {

        // UWAGA ISS Musi mieć uprawnienia do zapisu na serwerze

        // string outputDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Generated");
        //  string fileNameWithFullPath = Path.Combine(outputDirectory, "ExcelFiles",fileName);

        string fileName = "structures_to_import.csv";
        string fileNameWithFullPath = $@"{_path}\{fileName}";

        var structures = request.ComparedStructures;
        await AddHeading(fileNameWithFullPath);

        foreach (var structure in structures)
        {
            string line1a = $"KT1;{structure.PartNo};{structure.RevisionNo};{structure.AlternativeNo};{structure.LineSequence};";
            string line1b = $"{structure.MwComponentPart};{structure.MwQtyPerAssembly};{structure.MwShrinkageFactor};{structure.MwConsumptionItem};";

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
