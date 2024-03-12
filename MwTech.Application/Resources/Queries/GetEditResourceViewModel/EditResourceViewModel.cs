using MwTech.Application.Resources.Commands.EditResource;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Resources.Queries.GetEditResourceViewModel;

public class EditResourceViewModel
{
    public EditResourceCommand EditResourceCommand { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    public List<Unit> Units { get; set; }
    public GetResourcesViewModel GetResourcesViewModel { get; set; }

}
