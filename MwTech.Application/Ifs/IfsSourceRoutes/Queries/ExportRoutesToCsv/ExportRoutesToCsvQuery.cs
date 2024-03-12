using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesToCsv;

public class ExportRoutesToCsvQuery : IRequest
{
    public IEnumerable<ComparedRouteIfsVsMwTech> ComparedRoutes { get; set; }
}
