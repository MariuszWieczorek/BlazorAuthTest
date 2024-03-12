using MwTech.Application.Props.Commands.AddProperty;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Props.Queries.GetAddPropertyViewModel;

public class AddPropertyViewModel
{
    public AddPropertyCommand AddPropertyCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public List<Unit> Units { get; set; }

}
