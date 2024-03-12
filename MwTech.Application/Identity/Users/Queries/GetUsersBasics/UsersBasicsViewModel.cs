using MwTech.Application.Common.Models;
using MwTech.Domain.Entities;
namespace MwTech.Application.Identity.Users.Queries.GetUsersBasics;

public class UsersBasicsViewModel
{
    public IEnumerable<UserBasicsDto> Users { get; set; }
    public UserFilter UserFilter { get; set; }
    public PagingInfo PagingInfo { get; set; }

}
