using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Ifs.IfsScadaOperations.Queries.GetIfsOperations;

public class IfsScadaOperationsViewModel
{
    public IEnumerable<IfsScadaOperation> IfsScadaOperations { get; set; }
    public IfsScadaOperationFilter IfsScadaOperationFilter { get; set; }

}
