﻿using MwTech.Application.Operations.Queries.GetAddOperationViewModel;
using MwTech.Application.Operations.Queries.GetOperations;
using MwTech.Application.Products.Products.Queries.GetProducts;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Application.Products.Routes.Commands.EditRoute;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MwTech.Application.RoutingTools.Queries.GetRoutingTools;

namespace MwTech.Application.Products.Routes.Queries.GetEditRouteViewModel;

public class EditRouteViewModel
{
    public EditRouteCommand EditRouteCommand { get; set; }
    public GetResourcesViewModel GetResourcesViewModel { get; set; }
    public GetOperationsViewModel GetOperationsViewModel { get; set; }

    public GetRoutingToolsViewModel GetRoutingToolsViewModel { get; set; }

    public List<ProductCategory> ProductCategories { get; set; }
}
