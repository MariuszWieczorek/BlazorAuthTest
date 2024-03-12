using Unit = MwTech.Domain.Entities.Unit;

namespace MwTech.Application.Units.Queries.GetUnits;

public class UnitsViewModel
{
    public IEnumerable<Unit> Units { get; set; }
    public UnitFilter UnitFilter { get; set; }

}
