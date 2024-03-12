using MwTech.Application.Operations.Commands.EditOperation;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Operations.Queries.GetEditOperationViewModel;

public class EditOperationViewModel
{
    public EditOperationCommand EditOperationCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public List<Unit> Units { get; set; }

}
