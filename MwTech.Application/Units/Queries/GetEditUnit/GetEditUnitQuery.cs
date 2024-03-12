using MediatR;
using MwTech.Application.Units.Commands.EditUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Units.Queries.GetUnits.GetEditUnit;

public class GetEditUnitQuery : IRequest<EditUnitCommand>
{
    public int Id { get; set; }
}
