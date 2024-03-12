

using MwTech.Domain.Entities;

namespace MwTech.Application.Common.Interfaces;

public interface IProductService
{
    List<Bom> CalculateWeight(List<Bom> boms);
    List<Bom> CalculatePhr(List<Bom> boms);
    Task<decimal> CalculateWeight(int productId, int productVersionId);
    Task<decimal> CalculatePhr(int productId, int productVersionId);
    Task<List<ComponentUsage>> GetComponentUsages(string productNumber);
}
