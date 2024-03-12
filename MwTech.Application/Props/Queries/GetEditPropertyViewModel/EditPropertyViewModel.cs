using MwTech.Application.Props.Commands.EditProperty;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Props.Queries.GetEditPropertyViewModel;

public class EditPropertyViewModel
{
    public EditPropertyCommand EditPropertyCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public List<Unit> Units { get; set; }

}
