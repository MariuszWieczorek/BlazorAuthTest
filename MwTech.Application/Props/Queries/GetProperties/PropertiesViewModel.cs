using MwTech.Domain.Entities;
namespace MwTech.Application.Props.Queries.GetProperties;

public class PropertiesViewModel
{
    public IEnumerable<Property> Properties { get; set; }
    public PropertyFilter PropertyFilter { get; set; }
    public IEnumerable<ProductCategory> ProductCategories { get; set; }

}
