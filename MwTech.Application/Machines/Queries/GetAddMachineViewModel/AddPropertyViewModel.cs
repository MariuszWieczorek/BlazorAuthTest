using MwTech.Application.Machines.Commands.AddMachine;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Machines.Queries.GetAddMachineViewModel;

public class AddMachineViewModel
{
    public AddMachineCommand AddMachineCommand { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }

}
