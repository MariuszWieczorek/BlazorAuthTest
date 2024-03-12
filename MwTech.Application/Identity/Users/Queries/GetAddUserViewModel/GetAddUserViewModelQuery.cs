using MediatR;

namespace MwTech.Application.Identity.Users.Queries.GetAddUserViewModel;

public class GetAddUserViewModelQuery : IRequest<AddUserViewModel>
{
    public string Id { get; set; }
    public string Tab { get; set; }
}
