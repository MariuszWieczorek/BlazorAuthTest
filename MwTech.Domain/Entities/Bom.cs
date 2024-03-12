using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

// Set jest produktem w określonej wersji
// Part jest Produktem

public class Bom
{
    public int Id { get; set; }
    public int OrdinalNumber { get; set; }
    public Product? Set { get; set; }
    public int SetId { get; set; }
    public ProductVersion? SetVersion { get; set; }
    public int SetVersionId { get; set; }
    public Product? Part { get; set; }
    public int PartId { get; set; }
    public decimal PartQty { get; set; }
    public string? Description { get; set; }
    public bool OnProductionOrder { get; set; }
    public bool DoNotIncludeInTkw { get; set; }
    public bool DoNotIncludeInWeight { get; set; }
    public bool DoNotExportToIfs { get; set; }
    public decimal Excess { get; set; }
    public int Layer { get; set; }

    [NotMapped]
    public decimal PartWeight { get; set; }
    [NotMapped]
    public decimal Cost { get; set; }
    [NotMapped]
    public decimal TotalCost { get; set; }
    [NotMapped]
    public decimal LabourCost { get; set; }
    [NotMapped]
    public decimal TotalLabourCost { get; set; }
    [NotMapped]
    public decimal MaterialCost { get; set; }
    [NotMapped]
    public decimal TotalMaterialCost { get; set; }
    [NotMapped]
    public decimal MarkupCost { get; set; }
    [NotMapped]
    public decimal TotalMarkupCost { get; set; }
    [NotMapped]
    public string? CostDescription { get; set; }
    [NotMapped]
    public DateTime? ImportedDate { get; set; }
    [NotMapped]
    public DateTime? CalculatedDate { get; set; }
    [NotMapped]
    public decimal Phr { get; set; }
}