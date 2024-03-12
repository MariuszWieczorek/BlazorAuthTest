using MediatR;
using MwTech.Application.Common.Models;
using MwTech.Application.Products.Common;
using MwTech.Domain.Entities;

namespace MwTech.Application.Products.Products.Queries.GetOkcSpec;

public class GetOkcSpecQuery : IRequest<OkcSpecViewModel>
{
    public OkcFilter OkcFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
