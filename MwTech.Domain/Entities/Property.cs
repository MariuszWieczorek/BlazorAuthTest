using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class Property
{
    public int Id { get; set; }
    public int OrdinalNo { get; set; }
    public string PropertyNumber { get; set; }
    public string ScadaPropertyNumber { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    public int ProductCategoryId { get; set; }
    public bool IsGeneralProperty { get; set; }
    public bool IsVersionProperty { get; set; }
    public Unit? Unit { get; set; }
    public int UnitId { get; set; }
    public int? DecimalPlaces { get; set; }
    public bool HideOnReport { get; set; }
    public ICollection<ProductVersionProperty> ProductVersionProperties { get; set; } = new HashSet<ProductVersionProperty>();
    public ICollection<ProductProperty> ProductProperties { get; set; } = new HashSet<ProductProperty>();
    public ICollection<ProductCategory> ProductCategories2 { get; set; } = new HashSet<ProductCategory>();

}
