using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Shared.Tyres.Tyres.Commands.DeleteTyre;

namespace MwTech.Application.Currencies.Commands.DeleteTyre;

public class DeleteTyreCommandHandler : IRequestHandler<DeleteTyreCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTyreCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(DeleteTyreCommand request, CancellationToken cancellationToken)
    {
        var tyreToDelete = _context.Tyres.Single(x => x.Id == request.Id);
        _context.Tyres.Remove(tyreToDelete);

        await _context.SaveChangesAsync(cancellationToken);
        return;
    }
}
