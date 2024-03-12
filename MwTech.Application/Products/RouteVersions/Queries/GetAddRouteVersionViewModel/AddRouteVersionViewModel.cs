using MwTech.Application.Products.Products.Commands.AddProduct;
using MwTech.Application.Products.RouteVersions.Commands.AddRouteVersion;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.RouteVersions.Queries.GetAddRouteVersionViewModel;

public class AddRouteVersionViewModel
{
    public AddRouteVersionCommand AddRouteVersionCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }

}
