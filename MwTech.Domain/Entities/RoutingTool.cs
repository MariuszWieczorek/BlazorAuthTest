using MwTech.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class RoutingTool
{
    public int Id { get; set; }
    public string ToolNumber { get; set; }
    public string? Name { get; set; }
    public ICollection<ManufactoringRoute> ManufactoringRoutes { get; set; } = new HashSet<ManufactoringRoute>();

}

