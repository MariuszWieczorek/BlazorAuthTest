using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Routes.Queries.GetRoutes;

public class RoutesViewModel
{
    public IEnumerable<ManufactoringRoute> Routes { get; set; }
    public IEnumerable<Operation> Operations { get; set; }
    public IEnumerable<Resource> Resources { get; set; }
    public IEnumerable<Resource> WorkCenters { get; set; }
}
