using MwTech.Application.Common.Interfaces;
using MediatR;
using System.Security.Claims;

namespace MwTech.Application.Identity.Claims.Queries.GetClaims;
public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, IEnumerable<Claim>>
{
    private readonly IUserClaimManagerService _claimManager;
    private readonly ICurrentUserService _currentUser;

    public GetClaimsQueryHandler(IUserClaimManagerService claimManager, ICurrentUserService currentUser)
    {
        _claimManager = claimManager;
        _currentUser = currentUser;
    }


    public async Task<IEnumerable<Claim>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
    {
        return _claimManager.GetClaims(request.User);
    }


}
