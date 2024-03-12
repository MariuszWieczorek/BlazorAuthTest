using MediatR;


namespace MwTech.Application.Identity.Users.Commands.DeleteUser;
public class DeleteUserCommand : IRequest
{
    public string Id { get; set; }
}
