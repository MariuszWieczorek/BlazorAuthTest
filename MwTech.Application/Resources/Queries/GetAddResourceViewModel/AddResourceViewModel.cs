using MwTech.Application.Resources.Commands.AddResource;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Resources.Queries.GetAddResourceViewModel;

public class AddResourceViewModel
{
    public AddResourceCommand AddResourceCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public List<Unit> Units { get; set; }

}
