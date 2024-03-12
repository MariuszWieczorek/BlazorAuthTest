using MediatR;
using MwTech.Shared.Tyres.Tyres.Models;

namespace MwTech.Shared.Tyres.Tyres.Queries.GetTyres;

public class GetTyresQuery : IRequest<TyresViewModel>
{
    public TyreFilter TyreFilter { get; set; }
    public string SearchValue { get; set; }
    public string OrderInfo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
