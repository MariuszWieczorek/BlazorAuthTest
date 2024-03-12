using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceStructures.Queries.ExportStructuresDifferencesToCsv;

public class ExportStructuresDifferencesToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedStructureIfsVsMwTech> ComparedStructures { get; set; }
}
