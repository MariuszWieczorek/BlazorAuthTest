using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Routes.Queries.GetRoutes;

public class RouteFilter
{
    public string ProductNumber { get; set; }
    public int OperationId { get; set; }
    public int WorkCenterId { get; set; }
    public int ResorceId { get; set; }
}
