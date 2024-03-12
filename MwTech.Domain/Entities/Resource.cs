using MwTech.Domain.Entities.Recipes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class Resource
{
    public int Id { get; set; }
    public string ResourceNumber { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    public int ProductCategoryId { get; set; }
    public decimal Cost { get; set; }
    public decimal Markup { get; set; }
    public int UnitId { get; set; }
    public Unit? Unit { get; set; }
    public decimal EstimatedCost { get; set; }
    public decimal EstimatedMarkup { get; set; }
    public string? Contract { get; set; }
    public string? LaborClassNo { get; set; }
    public Resource? LabourClass { get; set; }
    public int? LabourClassId { get; set; }

    public ICollection<ManufactoringRoute> ManufactoringRoutes { get; set; } = new HashSet<ManufactoringRoute>();
    public ICollection<ManufactoringRoute> ManufactoringWorkCenters { get; set; } = new HashSet<ManufactoringRoute>();
    public ICollection<ManufactoringRoute> ManufactoringChangeOvers { get; set; } = new HashSet<ManufactoringRoute>();
    public ICollection<ProductSettingVersion> ProductSettingVersions { get; set; } = new HashSet<ProductSettingVersion>();
    public ICollection<RecipeStage> RecipeStagesWorkCenters { get; set; } = new HashSet<RecipeStage>();
    public ICollection<RecipeStage> RecipeLabourClasses { get; set; } = new HashSet<RecipeStage>();

    public ICollection<RecipePositionsPackage> PackageWorkCenters { get; set; } = new HashSet<RecipePositionsPackage>();
    public ICollection<RecipePositionsPackage> PackageLabourClasses { get; set; } = new HashSet<RecipePositionsPackage>();
    public ICollection<Resource> WorkCenters { get; set; } = new HashSet<Resource>();

}

