using MwTech.Application.Common.Interfaces;
using MwTech.Application.Dictionaries;
using MediatR;
using MwTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Identity.Users.Extensions;

namespace MwTech.Application.Identity.Users.Queries.GetUsersBasics;
public class GetUsersBasicsQueryHandler : IRequestHandler<GetUsersBasicsQuery, UsersBasicsViewModel>
{
    private readonly IUserRoleManagerService _userRoleManagerService;
    private readonly IApplicationDbContext _context;

    public GetUsersBasicsQueryHandler(
        IUserRoleManagerService userRoleManagerService, IApplicationDbContext context)
    {
        _userRoleManagerService = userRoleManagerService;
        _context = context;
    }

    public async Task<UsersBasicsViewModel> Handle(GetUsersBasicsQuery request, CancellationToken cancellationToken)
    {
        /*
        var userss = (await _userRoleManagerService
            .GetUsersInRoleAsync(RolesDict.Klient))
            .Select(x => x.ToUserBasicsDto());
        */

        var users = _context.Users
            .OrderBy(x => x.UserName)
            .AsNoTracking()
            .AsQueryable();

        users = Filter(users, request.UserFilter);

        // stronicowanie
        if (request.PagingInfo != null)
        {

            request.PagingInfo.TotalItems = users.Count();
            request.PagingInfo.ItemsPerPage = 100;

            if (request.PagingInfo.ItemsPerPage > 0 && request.PagingInfo.TotalItems > 0)
                users = users
                    .Skip((request.PagingInfo.CurrentPage - 1) * request.PagingInfo.ItemsPerPage)
                    .Take(request.PagingInfo.ItemsPerPage);
        }

        var UsersList = await users
            .Select(x => x.ToUserBasicsDto())
            .ToListAsync();

        var vm = new UsersBasicsViewModel()
        {
            UserFilter = request.UserFilter,
            Users = UsersList,
            PagingInfo = request.PagingInfo
        };


        return vm;
    }

    public IQueryable<ApplicationUser> Filter(IQueryable<ApplicationUser> users, UserFilter filter)
    {
        if (filter != null)
        {
            if (!string.IsNullOrWhiteSpace(filter.UserName))
                users = users.Where(x => x.UserName.Contains(filter.UserName));
        }

        return users;
    }
}
