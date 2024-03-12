using MwTech.Domain.Entities.Measurements;
using MwTech.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string ProductNumber { get; set; }
    public string? OldProductNumber { get; set; }
    public int? TechCardNumber { get; set; }
    public string? Idx01 { get; set; }
    public string? Idx02 { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    [Required]
    public int ProductCategoryId { get; set; }
    public Unit? Unit { get; set; }
    [Required]
    public int UnitId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CreatedByUserId { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedByUserId { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    public bool ReturnedFromProd { get; set; }
    public bool NoCalculateTkw { get; set; }
    public bool IsActive { get; set; }
    public bool IsTest { get; set; }
    public bool Client { get; set; }
    public int MwbaseMatid { get; set; }
    public int MwbaseWyrobId { get; set; }
    public decimal ContentsOfRubber { get; set; }
    public decimal Density  { get; set; }
    public int ScalesId { get; set; }
    public int? DecimalPlaces { get; set; }
    public decimal? WeightTolerance  { get; set; }
    public string? Aps01 { get; set; }
    public string? Aps02 { get; set; }
    public string? Ean13Code { get; set; }


    [NotMapped]
    public decimal ProductWeight { get; set; }
    [NotMapped]
    public decimal Cost { get; set; }
    [NotMapped]
    public decimal MaterialCost { get; set; }
    [NotMapped]
    public decimal LabourCost { get; set; }
    [NotMapped]
    public decimal ProductLabourCost { get; set; } // koszt robocizny tylko z marszruty produktu 


    [NotMapped]
    public int SetsCounter { get; set; }

    [NotMapped]
    public int PartsCounter { get; set; }

    [NotMapped]
    public string? info { get; set; }

    public ICollection<ProductVersion> ProductVersions { get; set; } = new HashSet<ProductVersion>();
    public ICollection<ProductSettingVersion> ProductSetingVersions { get; set; } = new HashSet<ProductSettingVersion>();
    public ICollection<ProductPropertyVersion> ProductPropertyVersions { get; set; } = new HashSet<ProductPropertyVersion>();
    public ICollection<Bom> BomParts { get; set; } = new HashSet<Bom>();
    public ICollection<Bom> BomSets { get; set; } = new HashSet<Bom>();
    public ICollection<ProductCost> ProductCosts { get; set; } = new HashSet<ProductCost>();
    public ICollection<RouteVersion> RouteVersions { get; set; } = new HashSet<RouteVersion>();
    public ICollection<RecipePosition> RecipePositions { get; set; } = new HashSet<RecipePosition>();
    public ICollection<MeasurementHeader> MeasurementHeaders { get; set; } = new HashSet<MeasurementHeader>();
}
