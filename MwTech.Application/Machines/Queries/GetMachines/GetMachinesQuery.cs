using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Machines.Queries.GetMachines;

public class GetMachinesQuery : IRequest<GetMachinesViewModel>
{
    public MachineFilter MachineFilter { get; set; }
}
