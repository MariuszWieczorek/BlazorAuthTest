using MediatR;
using System.Security.Claims;

namespace MwTech.Application.Identity.Claims.Queries.GetClaims;
public class GetClaimsQuery : IRequest<IEnumerable<Claim>>
{
    public ClaimsPrincipal User { get; set; }
}
