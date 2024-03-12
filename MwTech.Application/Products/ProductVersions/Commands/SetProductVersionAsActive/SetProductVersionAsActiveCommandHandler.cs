using MediatR;
using MwTech.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MwTech.Application.Products.ProductVersions.Commands.SetProductVersionAsActive;


public class SetProductVersionAsActiveCommandHandler : IRequestHandler<SetProductVersionAsActiveCommand>
{
    private readonly IApplicationDbContext _context;

    public SetProductVersionAsActiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Handle(SetProductVersionAsActiveCommand request, CancellationToken cancellationToken)
    {
        var routeVersion = _context.ProductVersions
            .SingleOrDefault(x => x.Id == request.ProductVersionId && x.ProductId == request.ProductId);
                        
        routeVersion.IsActive = true;

        await _context.SaveChangesAsync();

        return;
    }
}
