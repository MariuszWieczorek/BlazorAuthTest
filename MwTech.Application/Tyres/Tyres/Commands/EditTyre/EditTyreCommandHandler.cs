using MediatR;
using Microsoft.EntityFrameworkCore;
using MwTech.Application.Common.Interfaces;
using MwTech.Shared.Tyres.Tyres.Commands.EditTyre;

namespace MwTech.Application.Tyres.Commands.AddTyre;

public class EditTyreCommandHandler : IRequestHandler<EditTyreCommand>
{
    private readonly IApplicationDbContext _context;

    public EditTyreCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditTyreCommand request, CancellationToken cancellationToken)
    {
        var tyreToUpdate = await _context.Tyres
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        tyreToUpdate.Name = request.Name;
        tyreToUpdate.TyreNumber = request.TyreNumber;
        tyreToUpdate.Description = request.Description;
        tyreToUpdate.LoadIndex = request.LoadIndex;
        tyreToUpdate.RimDiameterInInches = request.RimDiameterInInches;
        tyreToUpdate.PlyRating = request.PlyRating;

        _context.Tyres.Update(tyreToUpdate);
        await _context.SaveChangesAsync(cancellationToken);

        return;
    }
}
