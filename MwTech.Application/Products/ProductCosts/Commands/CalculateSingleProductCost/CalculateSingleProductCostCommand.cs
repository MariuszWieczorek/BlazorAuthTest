using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductCosts.Commands.CalculateSingleProductCost;

public class CalculateSingleProductCostCommand : IRequest
{
    public int ProductId { get; set; }
}
