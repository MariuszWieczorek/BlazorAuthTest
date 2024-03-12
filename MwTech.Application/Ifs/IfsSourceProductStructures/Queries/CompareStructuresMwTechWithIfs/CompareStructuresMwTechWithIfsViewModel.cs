using MwTech.Application.Common.Models;
using MwTech.Application.Ifs.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Ifs.IfsSourceProductStructures.Queries.CompareStructuresMwTechWithIfs;

public class CompareStructuresMwTechWithIfsViewModel
{
    public IEnumerable<ComparedStructureIfsVsMwTech> ComparedStructuresIfsVsMwTech { get; set; }
    public CompareStructureFilter CompareStructureFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
