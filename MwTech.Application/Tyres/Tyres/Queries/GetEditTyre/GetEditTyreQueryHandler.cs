using MediatR;
using MwTech.Application.Common.Interfaces;
using MwTech.Shared.Currencies.Commands.EditCurrency;
using MwTech.Shared.Tyres.Tyres.Commands.EditTyre;
using MwTech.Shared.Tyres.Tyres.Queries.GetEditTyre;

namespace MwTech.Application.Currencies.Queries.GetEditCurrency;

public class GetEditTyreQueryHandler : IRequestHandler<GetEditTyreQuery, EditTyreCommand>
{
    private readonly IApplicationDbContext _context;

    public GetEditTyreQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EditTyreCommand> Handle(GetEditTyreQuery request, CancellationToken cancellationToken)
    {

        var tyreToEdit =  _context.Tyres.SingleOrDefault(x => x.Id == request.Id);
        
        if (tyreToEdit == null)
        {
            return null; 
        }

        return new EditTyreCommand 
        {
            Id = tyreToEdit.Id,
            TyreNumber = tyreToEdit.TyreNumber,
            Name = tyreToEdit.Name,
            Description = tyreToEdit.Description,
            LoadIndex = tyreToEdit.LoadIndex,
            PlyRating  = tyreToEdit.PlyRating,
            RimDiameterInInches = tyreToEdit.RimDiameterInInches
        };
    }
    
}
