using MediatR;
using MwTech.Application.Products.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Boms.Queries.GetAddBomViewModel;

public class GetAddBomViewModelQuery : IRequest<AddBomViewModel>
{
    public int ProductVersionId { get; set; }
    public int ProductId { get; set; }
    public string Tab { get; set; }
    public ProductFilter ProductFilter { get; set; }

}
