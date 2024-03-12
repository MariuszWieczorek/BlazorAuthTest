using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetProductTechCardViewModel;

public class ProductTechCardViewModel
{
    public IEnumerable<ProductVersion> ProductVersions { get; set; }
    public IEnumerable<Bom> Boms { get; set; }
    public IEnumerable<RouteVersion> RouteVersions { get; set; }
    public IEnumerable<ProductSettingVersion> ProductSettingVersions { get; set; }
    public IEnumerable<ProductProperty> ProductProperties { get; set; }
    public Product Product { get; set; }
    public Product Idx01 { get; set; }
    public Product Idx02 { get; set; }

}
