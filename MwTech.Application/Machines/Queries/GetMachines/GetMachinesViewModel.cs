using MwTech.Domain.Entities;
namespace MwTech.Application.Machines.Queries.GetMachines;

public class GetMachinesViewModel
{
    public IEnumerable<Machine> Machines { get; set; }
    public MachineFilter MachineFilter { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }

}
