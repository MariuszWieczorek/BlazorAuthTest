using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;

namespace MwTech.Application.Ifs.IfsSourceProductStructures.Queries.CompareStructuresIfsWithMwTech;

public class CompareStructuresIfsWithMwTechCommand : IRequest<CompareStructuresIfsWithMwTechViewModel>
{
    public CompareStructureFilter CompareStructureFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
