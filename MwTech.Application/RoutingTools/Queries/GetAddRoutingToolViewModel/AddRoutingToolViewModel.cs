using MwTech.Application.RoutingTools.Commands.AddRoutingTool;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.RoutingTools.Queries.GetAddRoutingToolViewModel;

public class AddRoutingToolViewModel
{
    public AddRoutingToolCommand AddRoutingToolCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }

}
