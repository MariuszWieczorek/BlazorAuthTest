using MwTech.Application.RoutingTools.Commands.EditRoutingTool;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.RoutingTools.Queries.GetEditRoutingToolViewModel;

public class EditRoutingToolViewModel
{
    public EditRoutingToolCommand EditRoutingToolCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }

}
