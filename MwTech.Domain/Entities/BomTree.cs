using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class BomTree
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string SetGrp { get; set; }
    public string PartGrp { get; set; }
    public int Level { get; set; }
    
    // zestaw
    public int? SetProductId { get; set; }
    public int SetProductVersionId { get; set; }
    public string? SetProductNumber { get; set; }
    public decimal SetProductQty { get; set; }
    public int SetUnitId { get; set; }
    public string? SetUnit { get; set; }

    // komponent podstawowe
    public int PartProductId { get; set; }
    public int PartProductVersionId { get; set; }
    public int PartOrdinalNo { get; set; }
    public string? PartProductNumber { get; set; }
    public string? PartProductName { get; set; }
    public int PartUnitId { get; set; }
    public string? PartUnit { get; set; }
    public decimal PartProductQty { get; set; }
    
    // ilości przeliczone względem węzła nadrzędnego
    public decimal FinalPartProductQty { get; set; }
    public decimal FinalPartProductWeight { get; set; }
    
    // komponent dodatkowe
    public decimal? PartContentsOfRubber { get; set; }
    public decimal? PartDensity { get; set; }
    public int PartScalesId { get; set; }
    public bool PartOnProductionOrder { get; set; }
    public bool PartDoesNotIncludeInWeight { get; set; }
    

    // z ilu kolejnych komponentów składa się komponent
    public int HowManyParts { get; set; }
    

    [NotMapped]
    public decimal? PartRubberQty { get; set; }
    [NotMapped]
    public decimal? PartVolume { get; set; }
    [NotMapped]
    public decimal? PartPhr { get; set; }
    [NotMapped]
    public decimal? PartMaterialPrice { get; set; }
    [NotMapped]
    public decimal? PartMaterialCost { get; set; }

    // dodane 2023.05.11



    [NotMapped]
    public decimal PartCost { get; set; }
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
    public string? LabourData { get; set; }

}
