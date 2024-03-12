using Microsoft.AspNetCore.Identity;
using MwTech.Domain.Entities.Measurements;
using MwTech.Domain.Entities.Recipes;
using MwTech.Domain.Entities.Tyres;
using System.ComponentModel.DataAnnotations;

namespace MwTech.Domain.Entities;

// Uwaga: nie ma właściwości Id ponieważ jest ono w klasie IdentityUser
public class ApplicationUser : IdentityUser
{

    [Display(Name = "Imię")]
    public string? FirstName { get; set; }


    [Display(Name = "Nazwisko")]
    public string? LastName { get; set; }


    [Display(Name = "Stanowisko")]
    public string? Possition { get; set; }


    [Display(Name = "Numer ewidencyjny")]
    public string? ReferenceNumber { get; set; }

    [Display(Name = "Kod RFID")]
    public string? Rfid { get; set; }

    [Display(Name = "Admin")]
    public bool AdminRights { get; set; }

    [Display(Name = "Super Admin")]
    public bool SuperAdminRights { get; set; }

    public DateTime RegisterDateTime { get; set; }
    public bool IsDeleted { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public ICollection<Product> CreatedProducts { get; set; } = new HashSet<Product>();
    public ICollection<Product> ModifiedProducts { get; set; } = new HashSet<Product>();


    public ICollection<Recipe> CreatedRecipies { get; set; } = new HashSet<Recipe>();
    public ICollection<Recipe> ModifiedRecipies { get; set; } = new HashSet<Recipe>();

    public ICollection<Tyre> CreatedTyres { get; set; } = new HashSet<Tyre>();
    public ICollection<Tyre> ModifiedTyres { get; set; } = new HashSet<Tyre>();

    public ICollection<ProductVersion> CreatedProductVersions { get; set; } = new HashSet<ProductVersion>();
    public ICollection<ProductVersion> ModifiedProductVersions { get; set; } = new HashSet<ProductVersion>();
    public ICollection<ProductVersion> Accepted01ProductVersions { get; set; } = new HashSet<ProductVersion>();
    public ICollection<ProductVersion> Accepted02ProductVersions { get; set; } = new HashSet<ProductVersion>();


    public ICollection<RecipeVersion> CreatedRecipeVersions { get; set; } = new HashSet<RecipeVersion>();
    public ICollection<RecipeVersion> ModifiedRecipeVersions { get; set; } = new HashSet<RecipeVersion>();
    public ICollection<RecipeVersion> Accepted01RecipeVersions { get; set; } = new HashSet<RecipeVersion>();
    public ICollection<RecipeVersion> Accepted02RecipeVersions { get; set; } = new HashSet<RecipeVersion>();


    public ICollection<TyreVersion> CreatedTyreVersions { get; set; } = new HashSet<TyreVersion>();
    public ICollection<TyreVersion> ModifiedTyreVersions { get; set; } = new HashSet<TyreVersion>();
    public ICollection<TyreVersion> Accepted01TyreVersions { get; set; } = new HashSet<TyreVersion>();
    public ICollection<TyreVersion> Accepted02TyreVersions { get; set; } = new HashSet<TyreVersion>();


    public ICollection<ProductSettingVersion> CreatedProductSettingVersions { get; set; } = new HashSet<ProductSettingVersion>();
    public ICollection<ProductSettingVersion> Accepted01ProductSettingVersions { get; set; } = new HashSet<ProductSettingVersion>();
    public ICollection<ProductSettingVersion> Accepted02ProductSettingVersions { get; set; } = new HashSet<ProductSettingVersion>();
    public ICollection<ProductSettingVersion> Accepted03ProductSettingVersions { get; set; } = new HashSet<ProductSettingVersion>();

    public ICollection<ProductSettingVersionPosition> ModifiedProductSettingVersionsPositions { get; set; } = new HashSet<ProductSettingVersionPosition>();

    public ICollection<ProductCost> CreatedProductCosts { get; set; } = new HashSet<ProductCost>();
    public ICollection<ProductCost> ModifiedProductCosts { get; set; } = new HashSet<ProductCost>();

    // 
    public ICollection<RouteVersion> CreatedRouteVersions { get; set; } = new HashSet<RouteVersion>();
    public ICollection<RouteVersion> ModifiedRouteVersions { get; set; } = new HashSet<RouteVersion>();
    public ICollection<RouteVersion> Accepted01RouteVersions { get; set; } = new HashSet<RouteVersion>();
    public ICollection<RouteVersion> Accepted02RouteVersions { get; set; } = new HashSet<RouteVersion>();

    public ICollection<ProductPropertyVersion> CreatedProductPropertyVersions { get; set; } = new HashSet<ProductPropertyVersion>();
    public ICollection<ProductPropertyVersion> ModifiedProductPropertyVersions { get; set; } = new HashSet<ProductPropertyVersion>();
    public ICollection<ProductPropertyVersion> Accepted01ProductPropertyVersions { get; set; } = new HashSet<ProductPropertyVersion>();
    public ICollection<ProductPropertyVersion> Accepted02ProductPropertyVersions { get; set; } = new HashSet<ProductPropertyVersion>();

    public ICollection<MeasurementHeader> CreatedMeasurementHeaders { get; set; } = new HashSet<MeasurementHeader>();
    public ICollection<MeasurementHeader> ModifiedMeasurementHeaders { get; set; } = new HashSet<MeasurementHeader>();
}
