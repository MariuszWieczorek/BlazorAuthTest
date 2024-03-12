using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Units.Queries.GetUnits;

public class GetUnitsQuery : IRequest<UnitsViewModel>
{
    public UnitFilter UnitFilter { get; set; }
}
