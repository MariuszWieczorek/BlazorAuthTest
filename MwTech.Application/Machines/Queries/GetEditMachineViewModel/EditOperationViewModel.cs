using MwTech.Application.Machines.Commands.EditMachine;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Machines.Queries.GetEditMachineViewModel;

public class EditMachineViewModel
{
    public EditMachineCommand EditMachineCommand { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }

}
