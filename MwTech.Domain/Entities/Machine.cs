using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class Machine
{
    public int Id { get; set; }
    public string MachineNumber { get; set; }
    public string? ReferenceNumber { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int MachineCategoryId { get; set; }

    public MachineCategory? MachineCategory { get; set; }
    public ICollection<ProductSettingVersion> ProductSetingVersions { get; set; } = new HashSet<ProductSettingVersion>();

}
