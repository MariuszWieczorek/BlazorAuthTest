using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Props.Queries.GetProperties;

public class GetPropertiesQuery : IRequest<PropertiesViewModel>
{
    public PropertyFilter PropertyFilter { get; set; }
}
