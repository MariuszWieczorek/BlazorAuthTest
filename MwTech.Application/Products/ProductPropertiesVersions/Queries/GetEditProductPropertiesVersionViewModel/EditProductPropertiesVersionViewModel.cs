using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Application.Products.ProductPropertiesVersions.Commands.EditProductPropertiesVersion;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductPropertiesVersions.Queries.GetEditProductPropertiesVersionViewModel;

public class EditProductPropertiesVersionViewModel
{
    public EditProductPropertiesVersionCommand EditProductPropertiesVersionCommand { get; set; }
    public IEnumerable<ProductProperty> ProductProperties { get; set; }

}
