using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;

namespace MwTech.Application.Ifs.IfsSourceProductStructures.Queries.CompareStructuresMwTechWithIfs;

public class CompareStructuresMwTechWithIfsCommand : IRequest<CompareStructuresMwTechWithIfsViewModel>
{
    public CompareStructureFilter CompareStructureFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
