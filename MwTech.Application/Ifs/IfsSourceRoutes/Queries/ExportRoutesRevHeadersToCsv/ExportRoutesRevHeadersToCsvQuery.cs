using MediatR;
using MwTech.Domain.Entities;

namespace MwTech.Application.IfsSourceRoutes.Queries.ExportRoutesRevHeadersToCsv;

public class ExportRoutesRevHeadersToCsvQuery : IRequest<string>
{
    public IEnumerable<ComparedRouteIfsVsMwTech> ComparedRoutes { get; set; }
}
