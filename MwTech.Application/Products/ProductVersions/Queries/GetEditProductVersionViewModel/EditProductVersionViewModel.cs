using MwTech.Application.Products.Products.Commands.EditProduct;
using MwTech.Application.Products.ProductVersions.Commands.EditProductVersion;
using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Queries.GetEditProductVersionViewModel;

public class EditProductVersionViewModel
{
    public EditProductVersionCommand EditProductVersionCommand { get; set; }
    public IEnumerable<Bom> Boms { get; set; }
    public IEnumerable<BomTree> BomLeaves { get; set; }

}
