using MwTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Boms.Queries.GetBomTreeView;

public class GetBomTreeViewViewModel
{
    public IEnumerable<BomTree> BomsTree { get; set; }
    public string HtmlCode { get; set; }
}
