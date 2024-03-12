using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesToolsToCsv;

public class ExportRoutesToolsToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedRouteIfsVsMwTech> ComparedRoutes { get; set; }
}
