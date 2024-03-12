using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class Setting
{
    public int Id { get; set; }
    public string SettingNumber { get; set; }
    public string Name { get; set; }
    public int OrdinalNumber { get; set; }
    public string? Description { get; set; }
    
    public int SettingCategoryId { get; set; }
    public SettingCategory? SettingCategory { get; set; }

    public int MachineCategoryId { get; set; }
    public MachineCategory? MachineCategory { get; set; }
    
    public int UnitId { get; set; }
    public Unit? Unit { get; set; }
    public string? Text { get; set; }
    public decimal? MinValue { get; set; }
    public decimal? Value { get; set; }
    public decimal? MaxValue { get; set; }
    public bool IsEditable { get; set; }
    public bool IsActive { get; set; }
    public bool IsNumeric { get; set; }
    public bool AlwaysOnPrintout { get; set; } // Drukowane zawsze mimo możliwej wartości 0
    public bool HideOnPrintout { get; set; }

    public ICollection<ProductSettingVersionPosition> ProductSettingVersionPositions { get; set; } = new HashSet<ProductSettingVersionPosition>();

}
