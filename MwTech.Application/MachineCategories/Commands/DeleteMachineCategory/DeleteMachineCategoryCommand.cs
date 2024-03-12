using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.MachineCategories.Commands.DeleteMachineCategory;

public class DeleteMachineCategoryCommand : IRequest
{
    public int Id { get; set; }
}
