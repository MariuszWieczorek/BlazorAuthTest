using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.Products.Queries.GetEditProductHeaderViewModel;

public class GetEditProductHeaderViewModelQuery : IRequest<EditProductHeaderViewModel>
{
    public int ProductId { get; set; }
    public string? Tab { get; set; }
}
