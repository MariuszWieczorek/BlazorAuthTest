using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesAltHeadersToCsv;

public class ExportRoutesAltHeadersToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedRouteIfsVsMwTech> ComparedRoutes { get; set; }
}
