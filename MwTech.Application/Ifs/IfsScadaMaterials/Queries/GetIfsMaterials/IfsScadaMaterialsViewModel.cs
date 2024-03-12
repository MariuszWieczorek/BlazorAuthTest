using MwTech.Domain.Entities.Ifs;

namespace MwTech.Application.Ifs.IfsScadaMaterials.Queries.GetIfsMaterials;

public class IfsScadaMaterialsViewModel
{
    public IEnumerable<IfsScadaMaterial> IfsScadaMaterials { get; set; }
    public IfsScadaMaterialFilter IfsScadaMaterialFilter { get; set; }

}
