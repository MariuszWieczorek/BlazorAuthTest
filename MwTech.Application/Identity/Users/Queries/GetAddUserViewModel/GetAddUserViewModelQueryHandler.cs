using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Application.Identity.Users.Commands.AddUser;

namespace MwTech.Application.Identity.Users.Queries.GetAddUserViewModel;

public class GetAddUserViewModelQueryHandler : IRequestHandler<GetAddUserViewModelQuery, AddUserViewModel>
{
    private readonly IApplicationDbContext _context;

    public GetAddUserViewModelQueryHandler(
        IApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task<AddUserViewModel> Handle(GetAddUserViewModelQuery request, CancellationToken cancellationToken)
    {

        var addUserCommand = new AddUserCommand();

        var vm = new AddUserViewModel
        {
            AddUserCommand = addUserCommand
        };

        return vm;
    }
}




