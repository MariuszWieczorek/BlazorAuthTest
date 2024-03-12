using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Machines.Queries.GetEditMachineViewModel;

public class GetEditMachineViewModelQuery : IRequest<EditMachineViewModel>
{
    public int Id { get; set; }
}
