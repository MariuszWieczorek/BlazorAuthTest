

using MwTech.Domain.Entities;

namespace MwTech.Application.Common.Interfaces;

public interface IEstimatedProductCostService
{
    Task CaluclateEstimatedProductCost(int productId);
    Task<List<Product>> GetEstimatedProductsCost(List<Product> products);
    Task<List<Bom>> GetEstimatedBomComponentsCosts(List<Bom> boms);
    Task<List<ManufactoringRoute>> GetEstimatedRoutesCost(List<ManufactoringRoute> routes);
}
