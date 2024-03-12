using MediatR;
using MwTech.Shared.Tyres.Tyres.Commands.EditTyre;

namespace MwTech.Shared.Tyres.Tyres.Queries.GetEditTyre;

public class GetEditTyreQuery : IRequest<EditTyreCommand> 
{
    public int Id { get; set; }
}
