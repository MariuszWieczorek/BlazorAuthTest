using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceStructures.Queries.ExportStructuresToCsv;

public class ExportStructuresToCsvQuery : IRequest
{
    public IEnumerable<ComparedStructureIfsVsMwTech> ComparedStructures { get; set; }
}
