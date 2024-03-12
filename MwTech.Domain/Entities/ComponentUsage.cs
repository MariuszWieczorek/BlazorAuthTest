using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ComponentUsage
{
    public string? ProductNumber { get; set; }
    public string? ProductName { get; set; }
    public int? TechCardNumber { get; set; }
    public int Layer { get; set; }
}
