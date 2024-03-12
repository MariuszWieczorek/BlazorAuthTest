using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Application.Products.ProductSettingVersions.Commands.EditProductSettingVersion;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Queries.GetEditProductSettingVersionViewModel;

public class EditProductSettingVersionViewModel
{
    public EditProductSettingVersionCommand EditProductSettingVersionCommand { get; set; }
    public IEnumerable<ProductSettingVersionPosition> ProductSettingVersionPositions { get; set; }
    public List<Machine> Machines { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }
    public GetResourcesViewModel GetResourcesViewModel { get; set; }
    public string ReturnAddress { get; set; }
    public string Anchor { get; set; }
}
