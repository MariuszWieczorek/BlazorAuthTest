using MwTech.Application.Products.Products.Commands.AddProduct;
using MwTech.Application.Products.ProductSettingVersions.Commands.AddProductSettingVersion;
using MwTech.Application.Resources.Queries.GetResources;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductSettingVersions.Queries.GetAddProductSettingVersionViewModel;

public class AddProductSettingVersionViewModel
{
    public AddProductSettingVersionCommand AddProductSettingVersionCommand { get; set; }
    public List<Machine> Machines { get; set; }
    public List<MachineCategory> MachineCategories { get; set; }
    public GetResourcesViewModel GetResourcesViewModel { get; set; }
}
