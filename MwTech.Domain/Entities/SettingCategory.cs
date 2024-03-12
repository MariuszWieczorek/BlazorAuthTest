namespace MwTech.Domain.Entities;

public class SettingCategory
{
    public int Id { get; set; }
    public string SettingCategoryNumber { get; set; }
    public string Name { get; set; }
    public int OrdinalNumber { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public int MachineCategoryId { get; set; }
    public MachineCategory? MachineCategory { get; set; }
    public ICollection<Setting> Settings { get; set; } = new HashSet<Setting>();
}
