using MwTech.Domain.Entities;
using MwTech.Domain.Entities.Tyres;
using MwTech.Shared.Common.Models;
using MwTech.Shared.Tyres.Tyres.Dtos;
using MwTech.Shared.Tyres.Tyres.Models;

namespace MwTech.Shared.Tyres.Tyres.Queries.GetTyres;

public class TyresViewModel
{
    public PaginatedList<TyreDto> TyresDto { get; set; }
    public TyreFilter TyreFilter { get; set; }
}
