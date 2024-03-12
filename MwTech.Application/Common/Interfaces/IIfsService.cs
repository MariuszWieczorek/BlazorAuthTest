using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Common.Interfaces;

public interface IIfsService
{
    Task<IEnumerable<IfsInventoryPartInStock>> GetIfsInventoryPartInStock(string PartNo);
}
