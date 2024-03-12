using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MwTech.Domain.Entities;

public class PropertiesProductCategoriesMap
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public Property Property { get; set; }
    public int ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; }

    public ICollection<Property> Properties { get; set; } = new HashSet<Property>();
    public ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();
}
