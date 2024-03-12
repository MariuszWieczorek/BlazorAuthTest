using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Boms.Queries.GetKendoTreeView;

public class KendoTreeViewViewModel
{
    public IEnumerable<BomTree> BomsTree { get; set; }
    public int ProductId { get; set; }
    public int ProductVersionId { get; set; }
}
