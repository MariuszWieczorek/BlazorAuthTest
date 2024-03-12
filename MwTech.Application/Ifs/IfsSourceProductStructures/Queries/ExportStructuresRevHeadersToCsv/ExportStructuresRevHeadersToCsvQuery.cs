using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceStructures.Queries.ExportStructuresRevHeadersToCsv;

public class ExportStructuresRevHeadersToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedStructureIfsVsMwTech> ComparedStructures { get; set; }
}
