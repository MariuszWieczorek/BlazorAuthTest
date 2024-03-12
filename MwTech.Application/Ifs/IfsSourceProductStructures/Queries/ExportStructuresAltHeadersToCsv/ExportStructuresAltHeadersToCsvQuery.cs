using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceStructures.Queries.ExportStructuresAltHeadersToCsv;

public class ExportStructuresAltHeadersToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedStructureIfsVsMwTech> ComparedStructures { get; set; }
}
