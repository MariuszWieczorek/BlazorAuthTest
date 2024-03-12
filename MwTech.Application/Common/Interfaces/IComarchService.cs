using MwTech.Domain.Entities.Comarch;

namespace MwTech.Application.Common.Interfaces;

public interface IComarchService
{
    Task<IEnumerable<ComarchBom>> GetAllBoms();
    Task<IEnumerable<ComarchTwrKarta>> ComarchTest();
    Task<IEnumerable<ComarchTwrCost>> GetTwrCost();
    IEnumerable<ComarchLoopedIndex> GetLoopedIndexes();
}
