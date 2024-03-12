using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.ProductSettingsVersions.Queries.GetProductSettingsCardViewModel;

public class ProductSettingsCardViewModel
{
    public Product Product { get; set; }

    public ProductVersion ProductVersion { get; set; }
    public IEnumerable<Bom> Boms { get; set; }

    public RouteVersion RouteVersion { get; set; }
    public IEnumerable<ManufactoringRoute> Routes { get; set; }
    
    public ProductSettingVersion ProductSettingVersion { get; set; }
    public IEnumerable<ProductSettingVersionPosition> ProductSettingsVersionPositions { get; set; }

    public ProductPropertyVersion ProductPropertyVersion { get; set; }
    public IEnumerable<ProductProperty> ProductProperties { get; set; }
    
    

}
