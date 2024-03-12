using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class MachineCategory
{
    public int Id { get; set; }
    public string MachineCategoryNumber { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    public int? ProductCategoryId { get; set; }
    public ProductCategory? ProductCategory { get; set; }

    public ICollection<Machine> Machines { get; set; } = new HashSet<Machine>();
    public ICollection<ProductSettingVersion> ProductSetingVersions { get; set; } = new HashSet<ProductSettingVersion>();
    public ICollection<SettingCategory> SettingCategories { get; set; } = new HashSet<SettingCategory>();
    public ICollection<Setting> Settings { get; set; } = new HashSet<Setting>();

}
