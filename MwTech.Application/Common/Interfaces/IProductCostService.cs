

using MwTech.Domain.Entities;

namespace MwTech.Application.Common.Interfaces;

public interface IProductCostService
{
    Task CaluclateProductCost(int productId);
    Task CaluclateOnlyMaterialProductCost(int productId);
    Task<List<Product>> GetProductsCost(List<Product> products);
    Task<List<Bom>> GetBomComponentsCosts(List<Bom> boms);
    Task<List<BomTree>> GetBomTreesComponentsCosts(List<BomTree> bomTrees);
    Task<List<ManufactoringRoute>> GetRoutesCost(List<ManufactoringRoute> routes);
    decimal CalcutateToPln(decimal cost, Currency currency, int periodId);
    Task<decimal> GetProductPrice(int productId);
}
