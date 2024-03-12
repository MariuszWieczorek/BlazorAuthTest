using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class Unit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? UnitCode { get; set; }
    public string? Icon { get; set; }
    public bool Weight { get; set; }
    public bool Time { get; set; }
    public bool Cost { get; set; }
    public bool Boolean { get; set; }
    public int PeriodInSeconds { get; set; }

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    public ICollection<Operation> Operations { get; set; } = new HashSet<Operation>();
    public ICollection<Property> Properties { get; set; } = new HashSet<Property>();
    public ICollection<Setting> Settings { get; set; } = new HashSet<Setting>();
    public ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();


}
