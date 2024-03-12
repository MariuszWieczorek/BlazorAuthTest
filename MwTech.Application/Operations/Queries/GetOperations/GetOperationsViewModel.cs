using MwTech.Domain.Entities;
namespace MwTech.Application.Operations.Queries.GetOperations;

public class GetOperationsViewModel
{
    public IEnumerable<Operation> Operations { get; set; }
    public OperationFilter OperationFilter { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }

}
