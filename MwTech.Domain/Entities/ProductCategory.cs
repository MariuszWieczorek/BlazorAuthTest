using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class ProductCategory
{
    public int Id { get; set; }
    public int OrdinalNumber { get; set; }
    public string Name { get; set; }
    public int RouteSource { get; set; }
    public string? CategoryNumber { get; set; }
    public string? TechCardNumber { get; set; }
    public bool TkwCountExcess { get; set; }
    public bool NoCalculateTkw { get; set; }
    public string Description { get; set; }
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    public ICollection<SettingCategory> SettingCategories { get; set; } = new HashSet<SettingCategory>();
    public ICollection<MachineCategory> MachineCategories { get; set; } = new HashSet<MachineCategory>();
    public ICollection<Operation> Operations { get; set; } = new HashSet<Operation>();
    public ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();
    public ICollection<Property> Properties { get; set; } = new HashSet<Property>();
    public ICollection<Property> Properties2 { get; set; } = new HashSet<Property>();
    public ICollection<ManufactoringRoute> ManufactoringRoutes { get; set; } = new HashSet<ManufactoringRoute>();
    public ICollection<RouteVersion> RouteVersions { get; set; } = new HashSet<RouteVersion>();
}
