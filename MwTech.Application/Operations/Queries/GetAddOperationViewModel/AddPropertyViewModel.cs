using MwTech.Application.Operations.Commands.AddOperation;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Operations.Queries.GetAddOperationViewModel;

public class AddOperationViewModel
{
    public AddOperationCommand AddOperationCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public List<Unit> Units { get; set; }

}
