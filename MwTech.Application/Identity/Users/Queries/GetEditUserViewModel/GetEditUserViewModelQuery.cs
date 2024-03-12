using MediatR;

namespace MwTech.Application.Identity.Users.Queries.GetEditUserViewModel;

public class GetEditUserViewModelQuery : IRequest<EditUserViewModel>
{
    public string Id { get; set; }
    public string Tab { get; set; }
}
