using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceStructures.Queries.ExportStructuresDifferencesToExcel;

public class ExportStructuresDifferencesToExcelQuery : IRequest<string>
{
    public IEnumerable<ComparedStructureIfsVsMwTech> ComparedStructures { get; set; }
}
