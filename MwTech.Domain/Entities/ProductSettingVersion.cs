namespace MwTech.Domain.Entities;

public class ProductSettingVersion
{
    public int Id { get; set; }
    
    public int AlternativeNo { get; set; }
    public int ProductSettingVersionNumber { get; set; }
    public int Rev { get; set; }

    public bool DefaultVersion { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? MwBaseName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public Product? Product { get; set; }
    public int ProductId { get; set; }
    
    public MachineCategory? MachineCategory { get; set; }
    public int MachineCategoryId { get; set; }
    
    public Machine? Machine { get; set; }
    public int MachineId { get; set; }


    public Resource? WorkCenter { get; set; }
    public int WorkCenterId { get; set; }


    public bool IsAccepted01 { get; set; }
    public string? Accepted01ByUserId { get; set; }
    public ApplicationUser? Accepted01ByUser { get; set; }
    public DateTime? Accepted01Date { get; set; }


    public bool IsAccepted02 { get; set; }
    public string? Accepted02ByUserId { get; set; }
    public ApplicationUser? Accepted02ByUser { get; set; }
    public DateTime? Accepted02Date { get; set; }


    public bool IsAccepted03 { get; set; }
    public string? Accepted03ByUserId { get; set; }
    public ApplicationUser? Accepted03ByUser { get; set; }
    public DateTime? Accepted03Date { get; set; }


    public DateTime? CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }

    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedByUserId { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }

    public DateTime? LastCsvFileDate { get; set; }

    public int MwbaseId { get; set; }

    public ICollection<ProductSettingVersionPosition> ProductSettingVersionPositions { get; set; } = new HashSet<ProductSettingVersionPosition>();

}
