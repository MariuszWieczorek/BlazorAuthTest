using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Domain.Entities.Tyres;
using MwTech.Shared.Tyres.Tyres.Commands.AddTyre;

namespace MwTech.Application.Tyres.Commands.AddTyre;

public class AddTyreCommandHandler : IRequestHandler<AddTyreCommand>
{
    private readonly IApplicationDbContext _context;

    public AddTyreCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddTyreCommand request, CancellationToken cancellationToken)
    {
        var tyre = new Tyre();
        tyre.TyreNumber = request.TyreNumber;
        tyre.Name = request.Name;
        tyre.Description = request.Description;
        tyre.RimDiameterInInches = request.RimDiameterInInches;
        tyre.LoadIndex = request.LoadIndex;
        tyre.PlyRating = request.PlyRating;

        await _context.Tyres.AddAsync(tyre);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
