using MwTech.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace MwTech.Application.Identity.Users.Commands.DeleteUser;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userToDelete = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        _context.Users.Remove(userToDelete);
        // userToDelete.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
