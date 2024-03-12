using MediatR;
using MwTech.Application.Common.Models;

namespace MwTech.Application.Identity.Users.Queries.GetUsersBasics;
public class GetUsersBasicsQuery : IRequest<UsersBasicsViewModel>
{
    public UserFilter UserFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }
}
