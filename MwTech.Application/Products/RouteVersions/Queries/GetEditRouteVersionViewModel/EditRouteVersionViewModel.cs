using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Application.Products.RouteVersions.Commands.EditRouteVersion;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Queries.GetEditRouteVersionViewModel;

public class EditRouteVersionViewModel
{
    public EditRouteVersionCommand EditRouteVersionCommand { get; set; }
    public IEnumerable<ManufactoringRoute> Routes { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }

}
