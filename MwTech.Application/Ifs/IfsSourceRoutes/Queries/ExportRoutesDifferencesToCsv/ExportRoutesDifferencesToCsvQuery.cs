using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesDifferencesToCsv;

public class ExportRoutesDifferencesToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedRouteIfsVsMwTech> ComparedRoutes { get; set; }
}
