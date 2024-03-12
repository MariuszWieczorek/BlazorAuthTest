using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Commands.CalculateOnlyMaterialProductsCosts;

public class CalculateOnlyMaterialProductsCostsCommand : IRequest
{
    public string ProductCategoryNumber { get; set; }
}
