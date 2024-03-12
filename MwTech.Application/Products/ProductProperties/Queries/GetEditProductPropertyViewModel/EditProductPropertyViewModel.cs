using MwTech.Application.Products.ProductProperties.Commands.EditProductProperty;
using MwTech.Application.Props.Queries.GetProperties;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductProperties.Queries.GetEditProductPropertyViewModel;

public class EditProductPropertyViewModel
{
    public EditProductPropertyCommand EditProductPropertyCommand { get; set; }
    public ProductPropertyVersion ProductPropertiesVersion { get; set; }
    public IEnumerable<Property> Properties { get; set; }
    public PropertiesViewModel GetPropertiesQueryViewModel { get; set; }
}
